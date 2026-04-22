using GestorTareas.API.DTOs;
using GestorTareas.API.Models;
using GestorTareas.API.Repositories;
using System.Linq;

namespace GestorTareas.API.Services;

// Implementación del servicio.
// Responsabilidad: convertir entre DTOs y entidades, y delegar
// las operaciones de BD al repositorio.
public class TaskService : ITaskService
{
    // Depende de la interfaz, no de la implementación concreta.
    // Esto facilita el testing con mocks.
    private readonly ITaskRepository _repo;

    public TaskService(ITaskRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<TaskDto>> GetAllAsync(string? status)
    {
        var tasks = await _repo.GetAllAsync(status);
        // Select proyecta cada TaskItem a TaskDto usando el método privado MapToDto.
        return tasks.Select(MapToDto);
    }

    public async Task<TaskDto?> GetByIdAsync(int id)
    {
        var task = await _repo.GetByIdAsync(id);
        // Si no existe la tarea devuelve null, el Controller se encarga del 404.
        return task is null ? null : MapToDto(task);
    }

    public async Task<TaskDto> CreateAsync(CreateTaskDto dto)
    {
        // Convierte el DTO de entrada a entidad para pasársela al repositorio.
        var task = new TaskItem
        {
            Titulo      = dto.Titulo,
            Descripcion = dto.Descripcion,
            Estado      = dto.Estado,
            UserId      = dto.UserId
        };
        var created = await _repo.CreateAsync(task);
        return MapToDto(created);
    }

    public async Task<TaskDto?> UpdateAsync(int id, UpdateTaskDto dto)
    {
        var task = new TaskItem
        {
            Titulo      = dto.Titulo,
            Descripcion = dto.Descripcion,
            Estado      = dto.Estado,
            UserId      = dto.UserId
        };
        var updated = await _repo.UpdateAsync(id, task);
        return updated is null ? null : MapToDto(updated);
    }

    public async Task<bool> DeleteAsync(int id) => await _repo.DeleteAsync(id);

    // Método privado de mapeo — centraliza la conversión de entidad a DTO.
    // Si el modelo cambia, solo se modifica acá.
    private static TaskDto MapToDto(TaskItem t) => new()
    {
        Id           = t.Id,
        Titulo       = t.Titulo,
        Descripcion  = t.Descripcion,
        Estado       = t.Estado,
        UserId       = t.UserId,
        UserNombre   = t.User?.Nombre, // ?. evita NullReferenceException si User no fue cargado
        FechaCreacion = t.FechaCreacion
    };
}