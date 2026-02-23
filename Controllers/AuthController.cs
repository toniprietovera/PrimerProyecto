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
        public IActionResult Registro([FromBody] RegistroDTO dto)
        {
            var usuario = _authService.Registrar(dto);
            if (usuario == null)
                return BadRequest(new { message = "El email ya está registrado" });

            return Ok(new { message = "Usuario registrado correctamente" });
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
    }
}