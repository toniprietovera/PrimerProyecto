using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimerProyecto.DTOs;
using PrimerProyecto.Repositories;
using PrimerProyecto.Services;
using System;
using System.Security.Claims;

namespace PrimerProyecto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TareasController : ControllerBase
    {
        private readonly TareaService _tareaService;

        public TareasController(TareaService tareaService)
        {
            _tareaService = tareaService;
        }

        private int ObtenerUsuarioId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.Parse(userId!);
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] PaginacionParams paginacion)
        {
            var usuarioId = ObtenerUsuarioId();
            var resultado = _tareaService.GetAll(usuarioId, paginacion);
            return Ok(resultado);
        }

        [HttpPost]
        public IActionResult Crear([FromBody] TareaDTO dto)
        {
            var usuarioId = ObtenerUsuarioId();
            var tarea = _tareaService.Crear(dto, usuarioId);
            return CreatedAtAction(nameof(GetAll), new { id = tarea.Id }, tarea);
        }
        [HttpPut("{id}")]
        public IActionResult Actualizar(int id, [FromBody] TareaDTO dto)
        {
            var actualizado = _tareaService.Actualizar(id, dto);
            if(!actualizado) return NotFound(new { Message = "Tarea no encontrada" });
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            var eliminado = _tareaService.Eliminar(id);
            if(!eliminado) return NotFound(new { Message = "Tarea no encontrada" });
            return NoContent();
        }
        
        [HttpDelete("admin/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult EliminarTarea(int id)
        {
            var eliminado = _tareaService.Eliminar(id);
            if (!eliminado) return NotFound(new { message = "Tarea no encontrada" });
            return Ok(new { message = "Tarea eliminada por el administrador" });
        }        
    }
}