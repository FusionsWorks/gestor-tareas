using GestorTareas.API.DTOs;

namespace GestorTareas.API.Services;

// Interfaz del servicio — trabaja con DTOs, no con entidades.
// El Controller solo conoce esta interfaz, nunca la implementación concreta.
// Esto desacopla las capas: si mañana cambia la lógica de negocio,
// el Controller no necesita modificarse.
public interface ITaskService
{
    // Todos los métodos son asíncronos (Task<T>) para no bloquear
    // el hilo del servidor mientras espera respuesta de la BD.
    Task<IEnumerable<TaskDto>> GetAllAsync(string? status);
    Task<TaskDto?> GetByIdAsync(int id);       // nullable: devuelve null si no existe
    Task<TaskDto> CreateAsync(CreateTaskDto dto);
    Task<TaskDto?> UpdateAsync(int id, UpdateTaskDto dto); // nullable: devuelve null si no existe
    Task<bool> DeleteAsync(int id);            // bool: indica si se encontró y eliminó
}