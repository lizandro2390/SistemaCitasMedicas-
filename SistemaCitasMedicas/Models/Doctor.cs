namespace SistemaCitasMedicas.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public string Especialidad { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
    }
}