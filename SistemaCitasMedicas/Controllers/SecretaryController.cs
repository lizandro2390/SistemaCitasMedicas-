using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaCitasMedicas.Data;
using SistemaCitasMedicas.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCitasMedicas.Controllers
{
    [Authorize(Roles = "Secretary")]
    public class SecretaryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SecretaryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var citas = await _context.Citas.ToListAsync();
            return View(citas);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveAppointment(int id)
        {
            var cita = await _context.Citas.FindAsync(id);
            if (cita != null)
            {
                cita.Estado = "Aprobada";
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CancelAppointment(int id)
        {
            var cita = await _context.Citas.FindAsync(id);
            if (cita != null)
            {
                cita.Estado = "Cancelada";
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}