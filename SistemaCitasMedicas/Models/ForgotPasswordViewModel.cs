using System.ComponentModel.DataAnnotations;

namespace SistemaCitasMedicas.Models
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo no es válido.")]
        public string Email { get; set; }
    }
}