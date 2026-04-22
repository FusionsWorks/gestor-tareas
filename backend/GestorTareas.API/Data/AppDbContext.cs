using GestorTareas.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GestorTareas.API.Data;

// DbContext es la clase principal de EF Core.
// Representa la sesión con la base de datos y expone
// las tablas como propiedades DbSet<T>.
public class AppDbContext : DbContext
{
    // El constructor recibe las opciones de configuración (connection string, provider)
    // que se registran en Program.cs mediante inyección de dependencias.
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Cada DbSet representa una tabla en la BD.
    // EF Core usa estas propiedades para generar las migraciones.
    public DbSet<User> Users { get; set; }
    public DbSet<TaskItem> Tasks { get; set; }

    // OnModelCreating permite configurar el esquema con Fluent API,
    // complementando o sobreescribiendo las convenciones de EF Core.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Define la relación: una Task pertenece a un User (muchos a uno).
        // HasOne + WithMany + HasForeignKey construye la FK en la tabla Tasks.
        modelBuilder.Entity<TaskItem>()
            .HasOne(t => t.User)
            .WithMany(u => u.Tasks)
            .HasForeignKey(t => t.UserId);

        // Define el valor por defecto de Estado a nivel de BD,
        // además del valor por defecto que ya tiene el modelo en C#.
        modelBuilder.Entity<TaskItem>()
            .Property(t => t.Estado)
            .HasDefaultValue("pendiente");

        // Seed de usuarios iniciales.
        // HasData inserta estos registros en la migración —
        // EF Core se encarga de no duplicarlos si ya existen.
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Nombre = "Ana García",       Email = "ana@example.com",    FechaCreacion = DateTime.UtcNow },
            new User { Id = 2, Nombre = "Carlos López",     Email = "carlos@example.com", FechaCreacion = DateTime.UtcNow },
            new User { Id = 3, Nombre = "María Fernández",  Email = "maria@example.com",  FechaCreacion = DateTime.UtcNow }
        );
    }
}