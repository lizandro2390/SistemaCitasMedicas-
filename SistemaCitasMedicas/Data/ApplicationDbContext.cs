using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SistemaCitasMedicas.Models;

namespace SistemaCitasMedicas.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.SetCommandTimeout(300);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configurar relaciones y claves primarias
            builder.Entity<Doctor>().HasKey(d => d.Id);
            builder.Entity<Secretaria>().HasKey(s => s.Id);
            builder.Entity<Cita>().HasKey(c => c.Id);

            // Relación Cita con Paciente (IdentityUser)
            builder.Entity<Cita>()
                .HasOne(c => c.Paciente)
                .WithMany()
                .HasForeignKey(c => c.PacienteId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Cita con Doctor
            builder.Entity<Cita>()
                .HasOne(c => c.Doctor)
                .WithMany()
                .HasForeignKey(c => c.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Ignorar propiedades de navegación en la validación
            builder.Entity<Cita>().Ignore(c => c.Paciente);
            builder.Entity<Cita>().Ignore(c => c.Doctor);
        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Secretaria> Secretarias { get; set; }
        public DbSet<Cita> Citas { get; set; }
    }
}