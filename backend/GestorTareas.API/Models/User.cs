namespace GestorTareas.API.Models;
// La tabla Users en la base de datos.
// Cada propiedad mapea a una columna de la tabla.
// Se le dió un nombre en inglés para que sea más sencillo de identificar, eso no quiere decir que no se pueda modificar.
public class User
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    // Se asigna la fecha actual en UTC automáticamente al crear el objeto.
    // UTC evita problemas de zonas horarias entre el servidor y la BD.
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    // Propiedad de navegación: permite acceder a las tareas de un usuario
    // directamente desde el objeto (ej: usuario.Tasks).
    // EntityFramewok Core la usa para construir el JOIN entre Users y Tasks.
    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}