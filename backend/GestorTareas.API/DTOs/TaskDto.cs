namespace GestorTareas.API.DTOs;

public class TaskDto
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public int UserId { get; set; }
    public string? UserNombre { get; set; }
    public DateTime FechaCreacion { get; set; }
}

public class CreateTaskDto
{
    public string Titulo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Estado { get; set; } = "pendiente";
    public int UserId { get; set; }
}

public class UpdateTaskDto
{
    public string Titulo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public int UserId { get; set; }
}