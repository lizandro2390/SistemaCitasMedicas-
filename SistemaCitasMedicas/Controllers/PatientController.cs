using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaCitasMedicas.Data;
using SistemaCitasMedicas.Models;
using System;
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
            ViewBag.Doctors = await _context.Doctors.ToListAsync();
            return View(new Cita());
        }

        [HttpPost]
        public async Task<IActionResult> ScheduleAppointment(Cita model)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                model.PacienteId = userId;
                model.Estado = "Pendiente";
                model.Notas = model.Notas ?? ""; // Asegurar que Notas no sea null
                _context.Citas.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            // Si el modelo no es válido, volver a cargar los doctores y mostrar errores
            ViewBag.Doctors = await _context.Doctors.ToListAsync();
            return View(model);
        }
    }
}