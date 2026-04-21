using GestorTareas.API.DTOs;

namespace GestorTareas.API.Services;

public interface ITaskService
{
    Task<IEnumerable<TaskDto>> GetAllAsync(string? status);
    Task<TaskDto?> GetByIdAsync(int id);
    Task<TaskDto> CreateAsync(CreateTaskDto dto);
    Task<TaskDto?> UpdateAsync(int id, UpdateTaskDto dto);
    Task<bool> DeleteAsync(int id);
}