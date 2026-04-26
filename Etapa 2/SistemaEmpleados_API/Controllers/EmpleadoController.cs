using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaEmpleados_API.Interfaces;
using SistemaEmpleados_API.Models;

namespace SistemaEmpleados_API.Controllers;

[Authorize]  // ← protege TODOS los endpoints del controller
[ApiController]
[Route("api/[controller]")]
public class EmpleadoController : ControllerBase
{
    private readonly IEmpleadoService _service;

    public EmpleadoController(IEmpleadoService service)
    {
        _service = service;
    }

    // GET api/empleado
    [HttpGet]
    public async Task<ActionResult<List<EmpleadoDto>>> GetTodos()
    {
        var empleados = await _service.ObtenerTodosAsync();
        return Ok(empleados);
    }

    // GET api/empleado/1
    [HttpGet("{id}")]
    public async Task<ActionResult<EmpleadoDto>> GetPorId(int id)
    {
        var empleado = await _service.ObtenerPorIdAsync(id);
        if (empleado == null) return NotFound($"No existe empleado con ID {id}.");
        return Ok(empleado);
    }

    // POST api/empleado
    [HttpPost]
    public async Task<ActionResult<EmpleadoDto>> Crear(CrearEmpleadoDto dto)
    {
        var empleado = await _service.CrearAsync(dto);
        return CreatedAtAction(nameof(GetPorId), new { id = empleado.Id }, empleado);
    }

    // PUT api/empleado/1/salario
    [HttpPut("{id}/salario")]
    public async Task<ActionResult> ActualizarSalario(int id, ActualizarSalarioDto dto)
    {
        var ok = await _service.ActualizarSalarioAsync(id, dto);
        if (!ok) return NotFound($"No existe empleado con ID {id}.");
        return NoContent();
    }

    // DELETE api/empleado/1
    [HttpDelete("{id}")]
    public async Task<ActionResult> Eliminar(int id)
    {
        var ok = await _service.EliminarAsync(id);
        if (!ok) return NotFound($"No existe empleado con ID {id}.");
        return NoContent();
    }
}