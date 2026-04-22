namespace GestorTareas.API.Models;

// La tabla Tasks en la base de datos.
// Se llama TaskItem y no Task para evitar conflicto con
// System.Threading.Tasks.Task que es una clase propia de C#.
// Se le dió un nombre en inglés para que sea más sencillo de identificar, eso no quiere decir que no se pueda modificar.
public class TaskItem
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Estado { get; set; } = "pendiente"; 
    // Estado limitado a tres valores: pendiente, en-progreso, completada.
    // Se establece como "pendiente" de forma normal, cuando se crea la tarea por primera vez.
    // La validación del CHECK constraint está definida en la BD (001_schema.sql).
    public int UserId { get; set; }
    // Clave foránea hacia la tabla Users (la de usuarios).
    // EntityFramework Core la usa para construir la relación entre Tasks y Users.
    
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    public User User { get; set; } = null!;
    // Propiedad de navegación hacia el usuario dueño de la tarea.
    // null! indica que EF Core se encarga de cargarlo — no será null
    // cuando se consulte con .Include(t => t.User).

}