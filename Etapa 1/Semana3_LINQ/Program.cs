// ── DATOS SIMULADOS ────────────────────────────────

var empleados = new List<Empleado>
{
    new() { Id=1, Nombre="Ana Torres",     Departamento="Ventas",     Seniority="Senior", Salario=3000, FechaIngreso=new DateTime(2018,3,1) },
    new() { Id=2, Nombre="Luis Gomez",     Departamento="Ventas",     Seniority="Junior", Salario=1200, FechaIngreso=new DateTime(2023,1,15) },
    new() { Id=3, Nombre="Sofia Paz",      Departamento="IT",         Seniority="Semi",   Salario=2200, FechaIngreso=new DateTime(2020,6,10) },
    new() { Id=4, Nombre="Carlos Ruiz",    Departamento="IT",         Seniority="Senior", Salario=3500, FechaIngreso=new DateTime(2017,9,20) },
    new() { Id=5, Nombre="Maria Lopez",    Departamento="RRHH",       Seniority="Semi",   Salario=1800, FechaIngreso=new DateTime(2021,4,5) },
    new() { Id=6, Nombre="Diego Fernandez",Departamento="IT",         Seniority="Junior", Salario=1300, FechaIngreso=new DateTime(2024,2,1) },
    new() { Id=7, Nombre="Laura Mendez",   Departamento="Ventas",     Seniority="Semi",   Salario=1900, FechaIngreso=new DateTime(2019,11,30) },
    new() { Id=8, Nombre="Pablo Silva",    Departamento="RRHH",       Seniority="Junior", Salario=1100, FechaIngreso=new DateTime(2024,7,1) },
};

var ventas = new List<Venta>
{
    new() { EmpleadoId=1, Producto="Software A", Monto=5000, Fecha=new DateTime(2024,1,10) },
    new() { EmpleadoId=1, Producto="Software B", Monto=3200, Fecha=new DateTime(2024,2,5) },
    new() { EmpleadoId=2, Producto="Software A", Monto=1500, Fecha=new DateTime(2024,1,20) },
    new() { EmpleadoId=7, Producto="Software C", Monto=4100, Fecha=new DateTime(2024,3,15) },
    new() { EmpleadoId=7, Producto="Software A", Monto=2800, Fecha=new DateTime(2024,4,1) },
    new() { EmpleadoId=3, Producto="Software B", Monto=900,  Fecha=new DateTime(2024,2,28) },
    new() { EmpleadoId=4, Producto="Software C", Monto=6200, Fecha=new DateTime(2024,1,5) },
    new() { EmpleadoId=1, Producto="Software C", Monto=4800, Fecha=new DateTime(2024,5,10) },
};

// 1a — Empleados de IT ordenados por salario descendente
Console.WriteLine("=== IT por salario ===");
var itOrdenado = empleados
    .Where(e => e.Departamento == "IT")
    .OrderByDescending(e => e.Salario)
    .ToList();

foreach (var e in itOrdenado)
    Console.WriteLine($"{e.Nombre} — ${e.Salario}");

// 1b — Empleados que ingresaron después de 2021 y ganan menos de $2000
Console.WriteLine("\n=== Nuevos con salario bajo ===");
var nuevosBaratos = empleados
    .Where(e => e.FechaIngreso.Year > 2021 && e.Salario < 2000)
    .OrderBy(e => e.FechaIngreso)
    .Select(e => new { e.Nombre, e.Salario, Año = e.FechaIngreso.Year })
    .ToList();

foreach (var e in nuevosBaratos)
    Console.WriteLine($"{e.Nombre} — ${e.Salario} — ingresó en {e.Año}");

Console.WriteLine("\n===Todos los empleados Senior===");
var todosSenior = empleados
    .Where(e => e.Seniority == "Senior")
    .OrderBy(e => e.FechaIngreso)
    .Select(e=> new {e.Nombre, e.Departamento, AñosEnLaEmpresa = DateTime.Now.Year - e.FechaIngreso.Year})
    .ToList();

foreach (var e in todosSenior)
    Console.WriteLine($"{e.Nombre} - {e.Departamento} - años en la empresa {e.AñosEnLaEmpresa}");

// 2a — Empleados agrupados por departamento
Console.WriteLine("\n=== Empleados por departamento ===");
var porDepto = empleados
    .GroupBy(e => e.Departamento)
    .OrderBy(g => g.Key)
    .ToList();

foreach (var grupo in porDepto)
{
    Console.WriteLine($"\n{grupo.Key} ({grupo.Count()} empleados):");
    foreach (var emp in grupo.OrderBy(e => e.Nombre))
        Console.WriteLine($"  - {emp.Nombre} ({emp.Seniority}) — ${emp.Salario}");
}

// 2b — Estadísticas por departamento
Console.WriteLine("\n=== Estadísticas por departamento ===");
var statsDepto = empleados
    .GroupBy(e => e.Departamento)
    .Select(g => new
    {
        Departamento = g.Key,
        CantidadEmpleados = g.Count(),
        SalarioPromedio = g.Average(e => e.Salario),
        SalarioTotal = g.Sum(e => e.Salario),
        EmpleadoMejorPago = g.MaxBy(e => e.Salario).Nombre
    })
    .OrderByDescending(g => g.SalarioPromedio)
    .ToList();

foreach (var s in statsDepto)
    Console.WriteLine($"{s.Departamento} | {s.CantidadEmpleados} emp. | " +
                      $"Prom: ${s.SalarioPromedio:F0} | " +
                      $"Total: ${s.SalarioTotal:F0} | " +
                      $"Top: {s.EmpleadoMejorPago}");


Console.WriteLine("\n== Empleados por Seniority ==");
var empSeniority = empleados
    .GroupBy(e => e.Seniority)
    .Select(g => new
    {   
        Nivel = g.Key,
        CantidadEmpleadosSeniority = g.Count(),
        SalarioPromedioSeniority = g.Average(e => e.Salario),
        Nombres = string.Join(", ", g.Select(e => e.Nombre))
    })
    .OrderBy(g => g.Nivel)
    .ToList();

foreach (var s in empSeniority)
    Console.WriteLine($"{s.Nivel} | {s.CantidadEmpleadosSeniority} empleados | " +
                      $"Prom: ${s.SalarioPromedioSeniority:F0} | " +
                      $"Nombres: {s.Nombres}");


// 3a — Join básico: ventas con nombre del empleado
Console.WriteLine("\n=== Ventas con nombre del empleado ===");
var ventasConNombre = ventas
    .Join(
        empleados,                    // con qué lista unís
        v => v.EmpleadoId,            // clave de ventas
        e => e.Id,                    // clave de empleados
        (v, e) => new                 // qué devolvés de cada par
        {
            e.Nombre,
            e.Departamento,
            v.Producto,
            v.Monto,
            v.Fecha
        }
    )
    .OrderByDescending(x => x.Monto)
    .ToList();

foreach (var v in ventasConNombre)
    Console.WriteLine($"{v.Nombre} ({v.Departamento}) — {v.Producto} — ${v.Monto} — {v.Fecha:dd/MM/yyyy}");

// 3b — Total vendido por empleado, solo los que tienen ventas
Console.WriteLine("\n=== Ranking de vendedores ===");
var ranking = ventas
    .GroupBy(v => v.EmpleadoId)
    .Join(
        empleados,
        g => g.Key,
        e => e.Id,
        (g, e) => new
        {
            e.Nombre,
            e.Departamento,
            TotalVentas = g.Sum(v => v.Monto),
            CantidadVentas = g.Count(),
            PromedioVenta = g.Average(v => v.Monto)
        }
    )
    .OrderByDescending(x => x.TotalVentas)
    .ToList();

Console.WriteLine($"{"Nombre",-20} {"Depto",-10} {"Total",10} {"Cant",6} {"Promedio",10}");
Console.WriteLine(new string('─', 60));
foreach (var r in ranking)
    Console.WriteLine($"{r.Nombre,-20} {r.Departamento,-10} ${r.TotalVentas,9:F0} {r.CantidadVentas,6} ${r.PromedioVenta,9:F0}");

// ── MODELOS ────────────────────────────────────────
public class Empleado
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Departamento { get; set; }
    public string Seniority { get; set; }  // "Junior", "Semi", "Senior"
    public decimal Salario { get; set; }
    public DateTime FechaIngreso { get; set; }
    public List<Venta> Ventas { get; set; } = new();
}

public class Venta
{
    public int EmpleadoId { get; set; }
    public string Producto { get; set; }
    public decimal Monto { get; set; }
    public DateTime Fecha { get; set; }
}

