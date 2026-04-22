namespace GestorTareas.API.DTOs;

// DTO de salida: lo que la API devuelve al cliente.
// Incluye el nombre del usuario para evitar que el cliente
// tenga que hacer una segunda llamada a /api/users.
// Mismo caso que con los modelos, se le puso un nombre en inglés para hacer más facil el coding.
public class TaskDto
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public int UserId { get; set; }
    public string? UserNombre { get; set; } // nullable: puede no estar cargado
    public DateTime FechaCreacion { get; set; }
}

public class CreateTaskDto
// DTO de entrada para crear una tarea.
// No incluye Id ni FechaCreacion — los genera la BD automáticamente.
{
    public string Titulo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Estado { get; set; } = "pendiente";
    public int UserId { get; set; }
}

public class UpdateTaskDto
// DTO de entrada para actualizar una tarea existente.
// Similar a CreateTaskDto pero semánticamente indica una operación de edición.
{
    public string Titulo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public int UserId { get; set; }
}