using PrimerProyecto.Models;
using PrimerProyecto.DTOs;
using Microsoft.Extensions.Validation;

namespace PrimerProyecto.Repositories
{
    public class TareaRepository
    {
        private readonly AppDbContext _context;

        public TareaRepository(AppDbContext context) // Inyección de dependencias
        {
            _context = context;
        }

        //public List<Tarea> GetAll(int usuarioId) => _context.Tareas.Where(t => t.UsuarioId == usuarioId).ToList();
        public PaginacionResult<Tarea> GetAll(int usuarioId, PaginacionParams paginacion)
        {
            var query = _context.Tareas.Where(t => t.UsuarioId == usuarioId);

            if (paginacion.Completada.HasValue)
            {
                query = query.Where(t => t.Completada == paginacion.Completada.Value);   
            }

            if (!string.IsNullOrEmpty(paginacion.BuscarTexto))
            {
                query = query.Where(t => 
                t.Titulo.Contains(paginacion.BuscarTexto) || 
                t.Descripcion.Contains(paginacion.BuscarTexto));
            }

            var totalRegistros = query.Count();
            var tamanoPagina = paginacion.GetTamanoPagina();
            var totalPaginas = (int)Math.Ceiling(totalRegistros / (double)tamanoPagina);

            var datos = query
                .OrderByDescending(t => t.FechaCreacion)
                .Skip((paginacion.Pagina - 1) * tamanoPagina)
                .Take(tamanoPagina)
                .ToList();

            return new PaginacionResult<Tarea>
            {
                Datos = datos,
                PaginaActual = paginacion.Pagina,
                TamanoPagina = tamanoPagina,
                TotalRegistros = totalRegistros,
                TotalPaginas = totalPaginas
            };
        }
        public Tarea? FindById(int id) => _context.Tareas.Find(id);
        public Tarea Create(Tarea tarea)
        {
            _context.Tareas.Add(tarea);
            _context.SaveChanges();
            return tarea;
        }
        public bool Remove(int id)
        {
            var tarea = FindById(id);
            if (tarea == null) return false;
            _context.Tareas.Remove(tarea);
            _context.SaveChanges();
            return true;
        }
        public bool Update(Tarea tarea)
        {
            _context.Tareas.Update(tarea);
            _context.SaveChanges();
            return true;
        }
    }
}