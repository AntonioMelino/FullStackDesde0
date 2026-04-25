using ProyectoFinal_Etapa1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal_Etapa1.Interfaces;

public interface IEmpleadoService
{
    Task<List<Empleado>> ObtenerTodosAsync();
    Task<Empleado?> ObtenerPorIdAsync(int id);
    Task<Empleado> CrearAsync(string nombre, string departamento, decimal salario);
    Task<bool> ActualizarSalarioAsync(int id, decimal nuevoSalario);
    Task<bool> EliminarAsync(int id);
    Task<List<ReporteEmpleado>> ObtenerReporteAsync();
}