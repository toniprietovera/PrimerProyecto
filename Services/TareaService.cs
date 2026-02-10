using PrimerProyecto.DTOs;
using PrimerProyecto.Models;
using PrimerProyecto.Repositories;

namespace PrimerProyecto.Services
{
    public class TareaService
    {
        private readonly TareaRepository _repo = new();
        public List<Tarea> GetAll() => _repo.GetAll();
        public Tarea? FindById(int id) => _repo.FindById(id);
        public Tarea Crear(TareaDTO dto)
        {
            var tarea = new Tarea
            {
                Titulo = dto.Titulo,
                Descripcion = dto.Descripcion,
                Completada = dto.Completada
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
            return true;
        }
    }
}