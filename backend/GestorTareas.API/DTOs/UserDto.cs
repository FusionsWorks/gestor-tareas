namespace GestorTareas.API.DTOs;

// DTO de salida para usuarios.
// No expone FechaCreacion ni la colección de Tasks —
// solo lo que el frontend necesita para el selector de usuarios.
// Nombre en inglés para hacer más facil el coding.
public class UserDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class CreateUserDto
// DTO de entrada para crear un usuario.
// Solo requiere Nombre y Email — Id y FechaCreacion los genera la BD.
{
    public string Nombre { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}