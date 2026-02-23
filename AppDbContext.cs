using Microsoft.EntityFrameworkCore;
using PrimerProyecto.Models;

namespace PrimerProyecto
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}