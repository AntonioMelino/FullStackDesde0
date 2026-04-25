using ProyectoFinal_Etapa1.Interfaces;

namespace ProyectoFinal_Etapa1.UI;

public class Menu
{
    private readonly IEmpleadoService _service;

    public Menu(IEmpleadoService service)
    {
        _service = service;
    }

    public async Task EjecutarAsync()
    {
        string opcion;
        do
        {
            MostrarMenu();
            opcion = Console.ReadLine();
            Console.Clear();

            switch (opcion)
            {
                case "1": await VerTodosAsync(); break;
                case "2": await BuscarPorIdAsync(); break;
                case "3": await AgregarEmpleadoAsync(); break;
                case "4": await ActualizarSalarioAsync(); break;
                case "5": await EliminarEmpleadoAsync(); break;
                case "6": await VerReporteAsync(); break;
                case "7": Console.WriteLine("¡Hasta luego!"); break;
                default: Console.WriteLine("Opción inválida.\n"); break;
            }

        } while (opcion != "7");
    }

    // ── MENÚ ───────────────────────────────────────
    private void MostrarMenu()
    {
        Console.WriteLine("╔══════════════════════════════╗");
        Console.WriteLine("║   SISTEMA DE EMPLEADOS       ║");
        Console.WriteLine("╠══════════════════════════════╣");
        Console.WriteLine("║  1. Ver todos los empleados  ║");
        Console.WriteLine("║  2. Buscar por ID            ║");
        Console.WriteLine("║  3. Agregar empleado         ║");
        Console.WriteLine("║  4. Actualizar salario       ║");
        Console.WriteLine("║  5. Eliminar empleado        ║");
        Console.WriteLine("║  6. Ver reporte de ventas    ║");
        Console.WriteLine("║  7. Salir                    ║");
        Console.WriteLine("╚══════════════════════════════╝");
        Console.Write("Opción: ");
    }

    // ── OPCIONES ───────────────────────────────────
    private async Task VerTodosAsync()
    {
        var empleados = await _service.ObtenerTodosAsync();

        Console.WriteLine("=== EMPLEADOS ===\n");
        Console.WriteLine($"{"ID",-5} {"Nombre",-20} {"Depto",-10} {"Salario",10} {"Ventas",8}");
        Console.WriteLine(new string('─', 57));

        foreach (var e in empleados)
            Console.WriteLine($"{e.Id,-5} {e.Nombre,-20} {e.Departamento,-10} ${e.Salario,9:F0} {e.Ventas.Count,8}");

        Console.WriteLine($"\nTotal: {empleados.Count} empleados");
        Pausar();
    }

    private async Task BuscarPorIdAsync()
    {
        Console.Write("Ingresá el ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido."); Pausar(); return;
        }

        var emp = await _service.ObtenerPorIdAsync(id);
        if (emp == null)
        {
            Console.WriteLine($"No existe empleado con ID {id}."); Pausar(); return;
        }

        Console.WriteLine($"\nNombre:      {emp.Nombre}");
        Console.WriteLine($"Departamento:{emp.Departamento}");
        Console.WriteLine($"Salario:     ${emp.Salario:F2}");
        Console.WriteLine($"Ingreso:     {emp.FechaIngreso:dd/MM/yyyy}");
        Console.WriteLine($"Ventas:      {emp.Ventas.Count}");

        if (emp.Ventas.Any())
        {
            Console.WriteLine("\nDetalle de ventas:");
            foreach (var v in emp.Ventas.OrderBy(v => v.Fecha))
                Console.WriteLine($"  {v.Fecha:dd/MM/yyyy} — {v.Producto} — ${v.Monto:F0}");
        }
        Pausar();
    }

    private async Task AgregarEmpleadoAsync()
    {
        Console.WriteLine("=== NUEVO EMPLEADO ===\n");

        Console.Write("Nombre: ");
        string nombre = Console.ReadLine();

        Console.Write("Departamento: ");
        string depto = Console.ReadLine();

        Console.Write("Salario: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal salario))
        {
            Console.WriteLine("Salario inválido."); Pausar(); return;
        }

        var emp = await _service.CrearAsync(nombre, depto, salario);
        Console.WriteLine($"\nEmpleado creado con ID: {emp.Id}");
        Pausar();
    }

    private async Task ActualizarSalarioAsync()
    {
        Console.Write("ID del empleado: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido."); Pausar(); return;
        }

        Console.Write("Nuevo salario: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal salario))
        {
            Console.WriteLine("Salario inválido."); Pausar(); return;
        }

        bool ok = await _service.ActualizarSalarioAsync(id, salario);
        Console.WriteLine(ok ? "Salario actualizado." : $"No existe empleado con ID {id}.");
        Pausar();
    }

    private async Task EliminarEmpleadoAsync()
    {
        Console.Write("ID del empleado a eliminar: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido."); Pausar(); return;
        }

        Console.Write("¿Confirmás? (s/n): ");
        if (Console.ReadLine()?.ToLower() != "s")
        {
            Console.WriteLine("Cancelado."); Pausar(); return;
        }

        bool ok = await _service.EliminarAsync(id);
        Console.WriteLine(ok ? "Empleado eliminado." : $"No existe empleado con ID {id}.");
        Pausar();
    }

    private async Task VerReporteAsync()
    {
        var reporte = await _service.ObtenerReporteAsync();

        Console.WriteLine("=== REPORTE DE VENTAS ===\n");
        Console.WriteLine($"{"Nombre",-20} {"Depto",-10} {"Salario",10} {"Ventas",8} {"Total $",12} {"Años",6}");
        Console.WriteLine(new string('─', 70));

        foreach (var r in reporte)
            Console.WriteLine($"{r.Nombre,-20} {r.Departamento,-10} ${r.Salario,9:F0} {r.CantidadVentas,8} ${r.TotalVentas,11:F0} {r.AnosEnEmpresa,6}");

        Pausar();
    }

    private void Pausar()
    {
        Console.WriteLine("\nPresioná cualquier tecla para continuar...");
        Console.ReadKey();
        Console.Clear();
    }
}