namespace GestorTareas.API.Models;

public class TaskItem
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Estado { get; set; } = "pendiente"; // pendiente | en-progreso | completada
    public int UserId { get; set; }
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    public User User { get; set; } = null!;
}