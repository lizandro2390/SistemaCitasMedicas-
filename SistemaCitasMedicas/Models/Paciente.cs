using Microsoft.AspNetCore.Identity;

namespace SistemaCitasMedicas.Models
{
    public class Paciente : IdentityUser
    {
        public string NombreCompleto { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; }
    }
}