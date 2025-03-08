using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SistemaCitasMedicas.Models
{
    public class Cita
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El ID del paciente es obligatorio.")]
        public string PacienteId { get; set; } // ID del usuario paciente (IdentityUser.Id)

        [Required(ErrorMessage = "El ID del doctor es obligatorio.")]
        public int DoctorId { get; set; } // ID del doctor

        [Required(ErrorMessage = "La fecha y hora son obligatorias.")]
        public DateTime FechaHora { get; set; }

        public string Estado { get; set; } // No requerido aquí, se establece en el controlador

        [Required(ErrorMessage = "El consultorio es obligatorio.")]
        public string Consultorio { get; set; }

        [Required(ErrorMessage = "El motivo es obligatorio.")]
        public string Motivo { get; set; } // Requerido para el usuario

        public string Notas { get; set; } // Opcional, no requerido

        // Relaciones
        public IdentityUser Paciente { get; set; }
        public Doctor Doctor { get; set; }
    }
}