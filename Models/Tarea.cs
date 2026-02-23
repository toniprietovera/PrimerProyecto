namespace PrimerProyecto.Models
{
    public class Tarea
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public bool Completada { get; set; }

        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    }
}