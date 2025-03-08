using System.ComponentModel.DataAnnotations;

namespace SistemaCitasMedicas.Models
{
    public class CreateDoctorViewModel
    {
        [Required(ErrorMessage = "El nombre completo es obligatorio.")]
        public string NombreCompleto { get; set; }

        [Required(ErrorMessage = "La especialidad es obligatoria.")]
        public string Especialidad { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [Phone(ErrorMessage = "El teléfono no es válido.")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo no es válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El usuario es obligatorio.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}