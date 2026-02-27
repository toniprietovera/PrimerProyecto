using PrimerProyecto.DTOs;
using PrimerProyecto.Models;
using PrimerProyecto.Repositories;

namespace PrimerProyecto.Services
{
    public class AuthService
    {
        private readonly UsuarioRepository _usuarioRepo;
        private readonly EmailService _emailService;

        public AuthService(UsuarioRepository usuarioRepo, EmailService emailService)
        {
            _usuarioRepo = usuarioRepo;
            _emailService = emailService;
        }

        public async Task<Usuario?> Registrar(RegistroDTO dto)
        {
            // Verifica si el email ya existe
            var usuarioExistente = _usuarioRepo.FindByEmail(dto.Email);
            if (usuarioExistente != null) return null;

            // Genera token de confirmación
            var tokenConfirmacion = Guid.NewGuid().ToString();

            // Hashea la contraseña
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            // Crea el usuario
            var usuario = new Usuario
            {
                Email = dto.Email,
                PasswordHash = passwordHash,
                Rol = "Usuario",
                EmailConfirmado = false,
                TokenConfirmacion = tokenConfirmacion,
                TokenExpiracion = DateTime.UtcNow.AddHours(24) // Expira en 24 horas
            };

            var usuarioCreado = _usuarioRepo.Create(usuario);
            Console.WriteLine($"Usuario creado: {usuarioCreado.Email}, Token: {usuarioCreado.TokenConfirmacion}");

            // Envía el email de confirmación
            await _emailService.EnviarEmailConfirmacion(usuario.Email, tokenConfirmacion);

            return usuarioCreado;
        }

        public Usuario? Login(LoginDTO dto)
        {
            // Busca el usuario por email
            var usuario = _usuarioRepo.FindByEmail(dto.Email);
            if (usuario == null) return null;

            if (!usuario.EmailConfirmado) return null;

            // Verifica la contraseña
            bool passwordValida = BCrypt.Net.BCrypt.Verify(dto.Password, usuario.PasswordHash);
            if (!passwordValida) return null;

            return usuario;
        }
        public Usuario? ConfirmarEmail(string token)
        {
            var usuario = _usuarioRepo.FindByToken(token);
            if (usuario == null) return null;
            if (usuario.TokenExpiracion < DateTime.UtcNow) return null;

            usuario.EmailConfirmado = true;
            usuario.TokenConfirmacion = null;
            usuario.TokenExpiracion = null;

            _usuarioRepo.Update(usuario);

            return usuario;
        }
    }
}