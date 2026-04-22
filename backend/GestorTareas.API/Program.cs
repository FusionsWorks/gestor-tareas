using GestorTareas.API.Data;
using GestorTareas.API.Repositories;
using GestorTareas.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// Swagger para documentación interactiva de la API.
builder.Services.AddSwaggerGen();

// Registra el DbContext con la cadena de conexión del appsettings.json.
// AddDbContext usa Scoped por defecto: una instancia por request HTTP.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Inyección de dependencias — se registran las interfaces con sus implementaciones.
// Scoped: se crea una instancia por request, lo que es correcto para repositorios y servicios.
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

// Política de CORS que permite requests desde el frontend React (Vite corre en 5173).
// Sin esto el browser bloquea las llamadas a la API por la política de same-origin.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy.WithOrigins("http://localhost:5173") // puerto del front
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

// Swagger solo se expone en desarrollo — en producción no debería estar habilitado.
app.UseSwagger();
app.UseSwaggerUI();

// CORS debe ir antes de UseAuthorization y MapControllers para que aplique correctamente.
app.UseCors("AllowFrontend");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Al iniciar la aplicación, se ejecutan las migraciones pendientes automáticamente.
// Si la tabla Users está vacía, se insertan los datos de prueba (seed inicial).
// Esto evita tener que correr scripts SQL manualmente al clonar el proyecto.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();

    if (!db.Users.Any())
    {
        db.Users.AddRange(
            new GestorTareas.API.Models.User { Nombre = "Ana García",       Email = "ana@example.com" },
            new GestorTareas.API.Models.User { Nombre = "Carlos López",     Email = "carlos@example.com" },
            new GestorTareas.API.Models.User { Nombre = "María Fernández",  Email = "maria@example.com" }
        );
        db.SaveChanges();
    }
}

app.Run();