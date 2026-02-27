using Microsoft.AspNetCore.Mvc;
using PrimerProyecto.DTOs;
using PrimerProyecto.Services;

namespace PrimerProyecto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly JwtService _jwtService;

        public AuthController(AuthService authService, JwtService jwtService)
        {
            _authService = authService;
            _jwtService = jwtService;
        }

        [HttpPost("registro")]
        public async Task<IActionResult> Registro([FromBody] RegistroDTO dto)
        {
            var usuario = await _authService.Registrar(dto);
            if (usuario == null)
                return BadRequest(new { message = "El email ya está registrado" });

            return Ok(new { message = "Usuario registrado. Revisa tu email para confirmar tu cuenta." });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO dto)
        {
            var usuario = _authService.Login(dto);
            if (usuario == null)
                return Unauthorized(new { message = "Email o contraseña incorrectos" });

            var token = _jwtService.GenerarToken(usuario);
            return Ok(new { token });
        }
        [HttpGet("confirmar-email")]
        public IActionResult ConfirmarEmail([FromQuery] string token)
        {
            var usuario = _authService.ConfirmarEmail(token);

            if (usuario == null)
                return BadRequest(new { message = "Token inválido o expirado" });

            return Ok(new { message = "Email confirmado correctamente. Ya puedes iniciar sesión." });
        }
    }
}