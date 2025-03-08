using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaCitasMedicas.Data;
using SistemaCitasMedicas.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCitasMedicas.Controllers
{
    [Authorize(Roles = "Patient")]
    public class PatientController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public PatientController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var citas = await _context.Citas
                .Where(c => c.PacienteId == userId)
                .ToListAsync();
            return View(citas);
        }

        [HttpGet]
        public async Task<IActionResult> ScheduleAppointment()
        {
            var doctors = await _context.Doctors.ToListAsync();
            if (doctors == null || !doctors.Any())
            {
                return View("Error", new { Message = "No doctors available." });
            }
            ViewBag.Doctors = doctors;
            return View(new Cita());
        }

        [HttpPost]
        public async Task<IActionResult> ScheduleAppointment(Cita model)
        {
            // Depuración: Imprimir valores recibidos
            System.Diagnostics.Debug.WriteLine($"DoctorId: {model.DoctorId}");
            System.Diagnostics.Debug.WriteLine($"FechaHora: {model.FechaHora}");
            System.Diagnostics.Debug.WriteLine($"Consultorio: {model.Consultorio}");
            System.Diagnostics.Debug.WriteLine($"Motivo: {model.Motivo}");
            System.Diagnostics.Debug.WriteLine($"Notas: {model.Notas}");

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    System.Diagnostics.Debug.WriteLine(error.ErrorMessage);
                }
            }

            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                ModelState.AddModelError("PacienteId", "No se pudo obtener el ID del paciente. Asegúrese de estar autenticado.");
            }

            if (model.DoctorId <= 0)
            {
                ModelState.AddModelError("DoctorId", "Debe seleccionar un doctor válido.");
            }

            if (ModelState.IsValid)
            {
                model.PacienteId = userId ?? "UnknownUser"; // Forzar un valor por defecto si userId es null
                model.Estado = "Pendiente";
                model.Notas = model.Notas ?? "";

                _context.Citas.Add(model);
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al guardar la cita: " + ex.Message);
                }
            }

            ViewBag.Doctors = await _context.Doctors.ToListAsync();
            return View(model);
        }
    }
}