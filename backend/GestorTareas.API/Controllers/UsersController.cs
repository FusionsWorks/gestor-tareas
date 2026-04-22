using GestorTareas.API.Data;
using GestorTareas.API.DTOs;
using GestorTareas.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorTareas.API.Controllers;

// A diferencia de TasksController, este controller accede directamente
// al DbContext sin pasar por Service ni Repository.
// Decisión de diseño consciente: la lógica de usuarios es simple
// (sin reglas de negocio) y agregar capas extra no aportaría valor.
[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsersController(AppDbContext context)
    {
        _context = context;
    }

    // GET /api/users
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        // Select proyecta directamente a UserDto en la query SQL —
        // EF Core no carga la entidad completa, solo las columnas necesarias.
        var users = await _context.Users
            .Select(u => new UserDto { Id = u.Id, Nombre = u.Nombre, Email = u.Email })
            .ToListAsync();
        return Ok(users); // 200
    }

    // POST /api/users
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
    {
        // Validación manual en lugar de DataAnnotations —
        // suficiente para los requerimientos de este endpoint.
        if (string.IsNullOrWhiteSpace(dto.Nombre) || string.IsNullOrWhiteSpace(dto.Email))
            return BadRequest("Nombre y email son requeridos."); // 400

        var user = new User { Nombre = dto.Nombre, Email = dto.Email };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // 201 con el usuario creado en el body.
        return CreatedAtAction(nameof(GetAll), new UserDto { Id = user.Id, Nombre = user.Nombre, Email = user.Email });
    }
}