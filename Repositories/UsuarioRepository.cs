using PrimerProyecto.Models;

namespace PrimerProyecto.Repositories
{
    public class UsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public Usuario? FindByEmail(string email)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Email == email);
        }

        public Usuario Create(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return usuario;
        }
        public Usuario? FindById(int id)
        {
            return _context.Usuarios.Find(id);
        }
        public Usuario Remove(Usuario usuario)
        {
            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
            return usuario;
        }
    }
}