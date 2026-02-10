using PrimerProyecto.DTOs;
using PrimerProyecto.Repositories;
using PrimerProyecto.Services;
using System;

namespace PrimerProyecto.Controllers
{
    public class TareasController
    {
        private readonly TareaService _tareaService = new();

        public void RunDemo()
        {
            Console.WriteLine("=== DEMO DE API DE TAREAS ===");
            // Crear una nueva tarea
            var tarea1 = _tareaService.Crear(new TareaDTO
            {
                Titulo = "Aprender C#",
                Descripcion = "Completar el curso de C# en línea",
                Completada = false
            });

            foreach (var tarea in _tareaService.GetAll())
            {
                Console.WriteLine($"ID: {tarea.Id}, Título: {tarea.Titulo}, Completada: {tarea.Completada}");
            }
            tarea1.Completada = true;

            Console.WriteLine($"ID: {tarea1.Id}, Estado: {tarea1.Completada}");

            _tareaService.Actualizar(tarea1.Id, new TareaDTO
            {
                Titulo = tarea1.Titulo,
                Descripcion = tarea1.Descripcion,
                Completada = tarea1.Completada
            });
            _tareaService.Eliminar(tarea1.Id);

            var tareas = _tareaService.GetAll();
            if (tareas == null || tareas.Count == 0)
            {
                Console.WriteLine("No hay tareas disponibles.");
            }
        }
    }
}