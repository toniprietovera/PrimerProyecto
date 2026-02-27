namespace PrimerProyecto.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Rol { get; set; } = "Admin";
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        public bool EmailConfirmado { get; set; } = false;
        public string? TokenConfirmacion { get; set; }
        public DateTime? TokenExpiracion { get; set; }

        public List<Tarea> Tareas { get; set; } = new List<Tarea>();
    }
}