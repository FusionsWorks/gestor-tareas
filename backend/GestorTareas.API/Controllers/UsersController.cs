using GestorTareas.API.Data;
using GestorTareas.API.DTOs;
using GestorTareas.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorTareas.API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsersController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _context.Users
            .Select(u => new UserDto { Id = u.Id, Nombre = u.Nombre, Email = u.Email })
            .ToListAsync();
        return Ok(users);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Nombre) || string.IsNullOrWhiteSpace(dto.Email))
            return BadRequest("Nombre y email son requeridos.");

        var user = new User { Nombre = dto.Nombre, Email = dto.Email };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAll), new UserDto { Id = user.Id, Nombre = user.Nombre, Email = user.Email });
    }
}