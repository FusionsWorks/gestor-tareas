using GestorTareas.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GestorTareas.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<TaskItem> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskItem>()
            .HasOne(t => t.User)
            .WithMany(u => u.Tasks)
            .HasForeignKey(t => t.UserId);

        modelBuilder.Entity<TaskItem>()
            .Property(t => t.Estado)
            .HasDefaultValue("pendiente");

        // Seed de usuarios
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Nombre = "Ana García",      Email = "ana@example.com",    FechaCreacion = DateTime.UtcNow },
            new User { Id = 2, Nombre = "Carlos López",    Email = "carlos@example.com", FechaCreacion = DateTime.UtcNow },
            new User { Id = 3, Nombre = "María Fernández", Email = "maria@example.com",  FechaCreacion = DateTime.UtcNow }
        );
    }
}