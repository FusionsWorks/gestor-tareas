using GestorTareas.API.DTOs;
using GestorTareas.API.Models;
using GestorTareas.API.Repositories;

namespace GestorTareas.API.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repo;

    public TaskService(ITaskRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<TaskDto>> GetAllAsync(string? status)
    {
        var tasks = await _repo.GetAllAsync(status);
        return tasks.Select(MapToDto);
    }

    public async Task<TaskDto?> GetByIdAsync(int id)
    {
        var task = await _repo.GetByIdAsync(id);
        return task is null ? null : MapToDto(task);
    }

    public async Task<TaskDto> CreateAsync(CreateTaskDto dto)
    {
        var task = new TaskItem
        {
            Titulo = dto.Titulo,
            Descripcion = dto.Descripcion,
            Estado = dto.Estado,
            UserId = dto.UserId
        };
        var created = await _repo.CreateAsync(task);
        return MapToDto(created);
    }

    public async Task<TaskDto?> UpdateAsync(int id, UpdateTaskDto dto)
    {
        var task = new TaskItem
        {
            Titulo = dto.Titulo,
            Descripcion = dto.Descripcion,
            Estado = dto.Estado,
            UserId = dto.UserId
        };
        var updated = await _repo.UpdateAsync(id, task);
        return updated is null ? null : MapToDto(updated);
    }

    public async Task<bool> DeleteAsync(int id) => await _repo.DeleteAsync(id);

    private static TaskDto MapToDto(TaskItem t) => new()
    {
        Id = t.Id,
        Titulo = t.Titulo,
        Descripcion = t.Descripcion,
        Estado = t.Estado,
        UserId = t.UserId,
        UserNombre = t.User?.Nombre,
        FechaCreacion = t.FechaCreacion
    };
}