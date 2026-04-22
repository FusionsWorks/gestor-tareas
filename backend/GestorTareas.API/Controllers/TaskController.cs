using GestorTareas.API.DTOs;
using GestorTareas.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestorTareas.API.Controllers;

// [ApiController] habilita comportamientos automáticos:
// validación de ModelState, binding de parámetros, respuestas 400 automáticas.
[ApiController]
[Route("api/tasks")] // ruta base para todos los endpoints de este controller
public class TasksController : ControllerBase
{
    // Depende de la interfaz del servicio, no de la implementación.
    private readonly ITaskService _service;

    public TasksController(ITaskService service)
    {
        _service = service;
    }

    // GET /api/tasks
    // GET /api/tasks?status=pendiente
    // [FromQuery] indica que status viene del query string, no de la URL.
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? status)
    {
        var tasks = await _service.GetAllAsync(status);
        return Ok(tasks); // 200 con la lista en el body
    }

    // GET /api/tasks/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var task = await _service.GetByIdAsync(id);
        // NotFound devuelve 404, Ok devuelve 200.
        return task is null ? NotFound() : Ok(task);
    }

    // POST /api/tasks
    // [FromBody] indica que los datos vienen en el cuerpo del request como JSON.
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTaskDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState); // 400
        var created = await _service.CreateAsync(dto);
        // CreatedAtAction devuelve 201 con el header Location apuntando al nuevo recurso.
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT /api/tasks/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTaskDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState); // 400
        var updated = await _service.UpdateAsync(id, dto);
        return updated is null ? NotFound() : Ok(updated); // 404 o 200
    }

    // DELETE /api/tasks/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        // NoContent devuelve 204 — éxito sin body, estándar REST para DELETE.
        return deleted ? NoContent() : NotFound(); // 204 o 404
    }
}