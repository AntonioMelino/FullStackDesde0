using Microsoft.EntityFrameworkCore;
using ProyectoFinal_Etapa1.Data;
using ProyectoFinal_Etapa1.Interfaces;
using ProyectoFinal_Etapa1.Models;

namespace ProyectoFinal_Etapa1.Services;

public class EmpleadoService : IEmpleadoService
{
    private readonly EmpresaContext _db;

    public EmpleadoService(EmpresaContext db)
    {
        _db = db;
    }

    // ── READ ───────────────────────────────────────
    public async Task<List<Empleado>> ObtenerTodosAsync()
    {
        return await _db.Empleados
            .Include(e => e.Ventas)
            .OrderBy(e => e.Departamento)
            .ToListAsync();
    }

    public async Task<Empleado?> ObtenerPorIdAsync(int id)
    {
        return await _db.Empleados
            .Include(e => e.Ventas)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    // ── CREATE ─────────────────────────────────────
    public async Task<Empleado> CrearAsync(string nombre, string departamento, decimal salario)
    {
        var empleado = new Empleado
        {
            Nombre = nombre,
            Departamento = departamento,
            Salario = salario,
            FechaIngreso = DateTime.Now
        };

        await _db.Empleados.AddAsync(empleado);
        await _db.SaveChangesAsync();
        return empleado;
    }

    // ── UPDATE ─────────────────────────────────────
    public async Task<bool> ActualizarSalarioAsync(int id, decimal nuevoSalario)
    {
        var empleado = await _db.Empleados.FindAsync(id);
        if (empleado == null) return false;

        empleado.Salario = nuevoSalario;
        await _db.SaveChangesAsync();
        return true;
    }

    // ── DELETE ─────────────────────────────────────
    public async Task<bool> EliminarAsync(int id)
    {
        var empleado = await _db.Empleados.FindAsync(id);
        if (empleado == null) return false;

        _db.Empleados.Remove(empleado);
        await _db.SaveChangesAsync();
        return true;
    }

    // ── REPORTE ────────────────────────────────────
    public async Task<List<ReporteEmpleado>> ObtenerReporteAsync()
    {
        return await _db.Empleados
            .Include(e => e.Ventas)
            .Select(e => new ReporteEmpleado
            {
                Nombre = e.Nombre,
                Departamento = e.Departamento,
                Salario = e.Salario,
                TotalVentas = e.Ventas.Sum(v => v.Monto),
                CantidadVentas = e.Ventas.Count(),
                AnosEnEmpresa = DateTime.Now.Year - e.FechaIngreso.Year
            })
            .OrderByDescending(r => r.TotalVentas)
            .ToListAsync();
    }
}