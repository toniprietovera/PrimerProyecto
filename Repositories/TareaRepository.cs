using PrimerProyecto.Models;
using System.Collections.Generic;
using System.Linq;

namespace PrimerProyecto.Repositories
{
    public class TareaRepository
    {
        private readonly List<Tarea> _tareas = new();
        private int _nextId = 1;

        public List<Tarea> GetAll() => _tareas;

        public Tarea? FindById(int id) => _tareas.FirstOrDefault(t => t.Id == id);

        public Tarea Create(Tarea tarea)
        {
            tarea.Id = _nextId++;
            _tareas.Add(tarea);
            return tarea;
        }

        public bool Remove(int id) => _tareas.RemoveAll(t => t.Id == id) > 0;
        public bool Update(Tarea tarea)
        {
            var existing = FindById(tarea.Id);
            if (existing == null) return false;

            existing.Titulo = tarea.Titulo;
            existing.Descripcion = tarea.Descripcion;
            existing.Completada = tarea.Completada;
            return true;
        }
    }
}