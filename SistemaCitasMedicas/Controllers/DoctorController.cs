using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaCitasMedicas.Data;
using SistemaCitasMedicas.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCitasMedicas.Controllers
{
    [Authorize(Roles = "Doctor")]
    public class DoctorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DoctorController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var doctorId = _context.Doctors.FirstOrDefault(d => d.Correo == User.Identity.Name)?.Id;
            if (doctorId.HasValue)
            {
                var citas = await _context.Citas
                    .Where(c => c.DoctorId == doctorId.Value)
                    .ToListAsync();
                return View(citas);
            }
            return View(new List<Cita>());
        }

        [HttpPost]
        public async Task<IActionResult> CompleteAppointment(int id)
        {
            var cita = await _context.Citas.FindAsync(id);
            if (cita != null && cita.DoctorId == _context.Doctors.FirstOrDefault(d => d.Correo == User.Identity.Name)?.Id)
            {
                cita.Estado = "Completada";
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}