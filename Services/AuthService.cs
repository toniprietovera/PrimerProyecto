using PrimerProyecto.DTOs;
using PrimerProyecto.Models;
using PrimerProyecto.Repositories;

namespace PrimerProyecto.Services
{
    public class AuthService
    {
        private readonly UsuarioRepository _usuarioRepo;

        public AuthService(UsuarioRepository usuarioRepo)
        {
            _usuarioRepo = usuarioRepo;
        }

        public Usuario? Registrar(RegistroDTO dto)
        {
            // Verifica si el email ya existe
            var usuarioExistente = _usuarioRepo.FindByEmail(dto.Email);
            if (usuarioExistente != null) return null;

            // Hashea la contraseña
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            // Crea el usuario
            var usuario = new Usuario
            {
                Email = dto.Email,
                PasswordHash = passwordHash,
                Rol = "Usuario"
            };

            return _usuarioRepo.Create(usuario);
        }

        public Usuario? Login(LoginDTO dto)
        {
            // Busca el usuario por email
            var usuario = _usuarioRepo.FindByEmail(dto.Email);
            if (usuario == null) return null;

            // Verifica la contraseña
            bool passwordValida = BCrypt.Net.BCrypt.Verify(dto.Password, usuario.PasswordHash);
            if (!passwordValida) return null;

            return usuario;
        }
    }
}