using Microsoft.EntityFrameworkCore;
using SistemaEmpleados_API.Data;
using SistemaEmpleados_API.Interfaces;
using SistemaEmpleados_API.Models;

namespace SistemaEmpleados_API.Services;

public class EmpleadoService : IEmpleadoService
{
    private readonly EmpresaContext _db;

    public EmpleadoService(EmpresaContext db)
    {
        _db = db;
    }

    public async Task<List<EmpleadoDto>> ObtenerTodosAsync()
    {
        return await _db.Empleados
            .Include(e => e.Ventas)
            .Select(e => new EmpleadoDto
            {
                Id = e.Id,
                Nombre = e.Nombre,
                Departamento = e.Departamento,
                Salario = e.Salario,
                FechaIngreso = e.FechaIngreso,
                CantidadVentas = e.Ventas.Count(),
                TotalVentas = e.Ventas.Sum(v => v.Monto)
            })
            .OrderBy(e => e.Departamento)
            .ToListAsync();
    }

    public async Task<EmpleadoDto?> ObtenerPorIdAsync(int id)
    {
        return await _db.Empleados
            .Include(e => e.Ventas)
            .Where(e => e.Id == id)
            .Select(e => new EmpleadoDto
            {
                Id = e.Id,
                Nombre = e.Nombre,
                Departamento = e.Departamento,
                Salario = e.Salario,
                FechaIngreso = e.FechaIngreso,
                CantidadVentas = e.Ventas.Count(),
                TotalVentas = e.Ventas.Sum(v => v.Monto)
            })
            .FirstOrDefaultAsync();
    }

    public async Task<EmpleadoDto> CrearAsync(CrearEmpleadoDto dto)
    {
        var empleado = new Empleado
        {
            Nombre = dto.Nombre,
            Departamento = dto.Departamento,
            Salario = dto.Salario,
            FechaIngreso = DateTime.Now
        };

        await _db.Empleados.AddAsync(empleado);
        await _db.SaveChangesAsync();

        return new EmpleadoDto
        {
            Id = empleado.Id,
            Nombre = empleado.Nombre,
            Departamento = empleado.Departamento,
            Salario = empleado.Salario,
            FechaIngreso = empleado.FechaIngreso
        };
    }

    public async Task<bool> ActualizarSalarioAsync(int id, ActualizarSalarioDto dto)
    {
        var empleado = await _db.Empleados.FindAsync(id);
        if (empleado == null) return false;

        empleado.Salario = dto.NuevoSalario;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var empleado = await _db.Empleados.FindAsync(id);
        if (empleado == null) return false;

        _db.Empleados.Remove(empleado);
        await _db.SaveChangesAsync();
        return true;
    }
}