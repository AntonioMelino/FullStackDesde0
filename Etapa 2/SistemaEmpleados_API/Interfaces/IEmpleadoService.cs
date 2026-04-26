using SistemaEmpleados_API.Models;

namespace SistemaEmpleados_API.Interfaces;

public interface IEmpleadoService
{
    Task<List<EmpleadoDto>> ObtenerTodosAsync();
    Task<EmpleadoDto?> ObtenerPorIdAsync(int id);
    Task<EmpleadoDto> CrearAsync(CrearEmpleadoDto dto);
    Task<bool> ActualizarSalarioAsync(int id, ActualizarSalarioDto dto);
    Task<bool> EliminarAsync(int id);
}