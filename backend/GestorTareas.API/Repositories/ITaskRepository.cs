using GestorTareas.API.Models;

namespace GestorTareas.API.Repositories;

// Interfaz que define el contrato del repositorio.
// El Service solo conoce esta interfaz, no la implementación concreta.
// Esto permite cambiar la implementación (por ejemplo, de EF Core a Dapper)
// sin tocar el Service ni el Controller.
public interface ITaskRepository
{
    // El parámetro status es nullable — si es null, devuelve todas las tareas.
    Task<IEnumerable<TaskItem>> GetAllAsync(string? status);
    Task<TaskItem?> GetByIdAsync(int id);      // nullable: puede no existir
    Task<TaskItem> CreateAsync(TaskItem task);
    Task<TaskItem?> UpdateAsync(int id, TaskItem task); // nullable: puede no existir
    Task<bool> DeleteAsync(int id);            // bool: true si se eliminó, false si no existía
}