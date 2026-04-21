using GestorTareas.API.Data;
using GestorTareas.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GestorTareas.API.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaskItem>> GetAllAsync(string? status)
    {
        var query = _context.Tasks.Include(t => t.User).AsQueryable();

        if (!string.IsNullOrWhiteSpace(status))
            query = query.Where(t => t.Estado == status);

        return await query.ToListAsync();
    }

    public async Task<TaskItem?> GetByIdAsync(int id) =>
        await _context.Tasks.Include(t => t.User).FirstOrDefaultAsync(t => t.Id == id);

    public async Task<TaskItem> CreateAsync(TaskItem task)
    {
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task<TaskItem?> UpdateAsync(int id, TaskItem updated)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task is null) return null;

        task.Titulo = updated.Titulo;
        task.Descripcion = updated.Descripcion;
        task.Estado = updated.Estado;
        task.UserId = updated.UserId;

        await _context.SaveChangesAsync();
        return task;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task is null) return false;

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        return true;
    }
}