using PrimerProyecto.DTOs;
using PrimerProyecto.Models;
using PrimerProyecto.Repositories;

namespace PrimerProyecto.Services
{
    public class TareaService
    {
        private readonly TareaRepository _repo;
        public TareaService(TareaRepository repo) // Inyección de dependencias
        {
            _repo = repo;
        }
        public PaginacionResult<Tarea> GetAll(int usuarioId, PaginacionParams paginacion)
        {
            return _repo.GetAll(usuarioId, paginacion);
        }
        public Tarea? FindById(int id) => _repo.FindById(id);
        public Tarea Crear(TareaDTO dto, int usuarioId)
        {
            var tarea = new Tarea
            {
                Titulo = dto.Titulo,
                Descripcion = dto.Descripcion,
                Completada = dto.Completada,
                UsuarioId = usuarioId
            };
            return _repo.Create(tarea);
        }
        public bool Eliminar(int id) => _repo.Remove(id);
        public bool Actualizar(int id, TareaDTO dto)
        {
            var tarea = _repo.FindById(id);
            if (tarea == null) return false;

            tarea.Titulo = dto.Titulo;
            tarea.Descripcion = dto.Descripcion;
            tarea.Completada = dto.Completada;
            return _repo.Update(tarea);
        }
    }
}