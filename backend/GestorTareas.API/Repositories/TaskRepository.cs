using GestorTareas.API.Data;
using GestorTareas.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GestorTareas.API.Repositories;

// Implementación concreta del repositorio.
// Es la única capa que habla directamente con la base de datos via EF Core.
// El resto de las capas no saben que existe SQL Server.
public class TaskRepository : ITaskRepository
{
    // El DbContext se inyecta por constructor — no se instancia manualmente.
    // ASP.NET Core se encarga del ciclo de vida (Scoped por request).
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaskItem>> GetAllAsync(string? status)
    {
        // AsQueryable permite construir la query dinámicamente
        // antes de ejecutarla contra la BD.
        // Include carga el User relacionado en el mismo query (JOIN).
        var query = _context.Tasks.Include(t => t.User).AsQueryable();

        // Si se pasó un filtro de estado, se agrega el WHERE a la query.
        // Si status es null o vacío, se devuelven todas las tareas.
        if (!string.IsNullOrWhiteSpace(status))
            query = query.Where(t => t.Estado == status);

        // ToListAsync ejecuta la query contra la BD de forma asíncrona.
        return await query.ToListAsync();
    }

    public async Task<TaskItem?> GetByIdAsync(int id) =>
        // FirstOrDefaultAsync devuelve null si no encuentra el registro,
        // en lugar de lanzar una excepción como First haría.
        await _context.Tasks.Include(t => t.User).FirstOrDefaultAsync(t => t.Id == id);

    public async Task<TaskItem> CreateAsync(TaskItem task)
    {
        // Add marca la entidad como Added en el tracker de EF Core.
        _context.Tasks.Add(task);
        // SaveChangesAsync genera y ejecuta el INSERT en la BD.
        await _context.SaveChangesAsync();
        // Devuelve la tarea con el Id generado por la BD.
        return task;
    }

    public async Task<TaskItem?> UpdateAsync(int id, TaskItem updated)
    {
        // FindAsync busca por PK — más eficiente que FirstOrDefault
        // porque EF Core primero revisa el cache local antes de ir a la BD.
        var task = await _context.Tasks.FindAsync(id);
        if (task is null) return null;

        // Se actualizan solo los campos editables.
        // Id y FechaCreacion no se tocan.
        task.Titulo      = updated.Titulo;
        task.Descripcion = updated.Descripcion;
        task.Estado      = updated.Estado;
        task.UserId      = updated.UserId;

        // SaveChangesAsync genera el UPDATE solo para las propiedades modificadas.
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task is null) return false;

        // Remove marca la entidad como Deleted en el tracker.
        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        return true;
    }
}