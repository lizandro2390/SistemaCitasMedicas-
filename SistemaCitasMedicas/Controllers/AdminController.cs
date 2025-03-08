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
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Patients()
        {
            var patients = await _userManager.GetUsersInRoleAsync("Patient");
            return View(patients);
        }

        public async Task<IActionResult> Doctors()
        {
            var doctors = await _context.Doctors.ToListAsync();
            return View(doctors);
        }

        public async Task<IActionResult> Secretaries()
        {
            var secretaries = await _context.Secretarias.ToListAsync();
            return View(secretaries);
        }

        [HttpGet]
        public IActionResult CreateDoctor()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDoctor(CreateDoctorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Username, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Doctor");
                    var doctor = new Doctor
                    {
                        NombreCompleto = model.NombreCompleto,
                        Especialidad = model.Especialidad,
                        Telefono = model.Telefono,
                        Correo = model.Email
                    };
                    _context.Doctors.Add(doctor);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult CreateSecretary()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSecretary(CreateSecretaryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Username, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Secretary");
                    var secretary = new Secretaria
                    {
                        NombreCompleto = model.NombreCompleto,
                        Telefono = model.Telefono,
                        Correo = model.Email
                    };
                    _context.Secretarias.Add(secretary);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
    }
}