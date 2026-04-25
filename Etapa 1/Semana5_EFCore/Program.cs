using Microsoft.EntityFrameworkCore;

// ── PROGRAM ────────────────────────────────────────

using var db = new EmpresaContext();
await db.Database.EnsureCreatedAsync();

await AgregarVentasAsync();
await MostrarEmpleadosConVentasAsync();
await MostrarEstadisticasAsync();

// ── AGREGAR VENTAS ─────────────────────────────────
async Task AgregarVentasAsync()
{
    if (await db.Ventas.AnyAsync()) return;

    Console.WriteLine("Agregando ventas...");

    // Primero buscamos los empleados existentes
    var ana = await db.Empleados.FirstOrDefaultAsync(e => e.Nombre == "Ana Torres");
    var sofia = await db.Empleados.FirstOrDefaultAsync(e => e.Nombre == "Sofia Paz");
    var carlos = await db.Empleados.FirstOrDefaultAsync(e => e.Nombre == "Carlos Ruiz");

    if (ana == null || sofia == null || carlos == null)
    {
        Console.WriteLine("Faltan empleados. Corré el programa anterior primero.");
        return;
    }

    var ventas = new List<Venta>
    {
        new() { EmpleadoId=ana.Id,    Producto="Software A", Monto=5000, Fecha=new DateTime(2024,1,10) },
        new() { EmpleadoId=ana.Id,    Producto="Software B", Monto=3200, Fecha=new DateTime(2024,2,5)  },
        new() { EmpleadoId=ana.Id,    Producto="Software C", Monto=4800, Fecha=new DateTime(2024,5,10) },
        new() { EmpleadoId=sofia.Id,  Producto="Software B", Monto=900,  Fecha=new DateTime(2024,2,28) },
        new() { EmpleadoId=carlos.Id, Producto="Software A", Monto=2100, Fecha=new DateTime(2024,3,15) },
        new() { EmpleadoId=carlos.Id, Producto="Software C", Monto=6200, Fecha=new DateTime(2024,1,5)  },
    };

    await db.Ventas.AddRangeAsync(ventas);
    await db.SaveChangesAsync();
    Console.WriteLine($"{ventas.Count} ventas agregadas.\n");
}

// ── READ CON INCLUDE ───────────────────────────────
async Task MostrarEmpleadosConVentasAsync()
{
    Console.WriteLine("=== EMPLEADOS CON VENTAS ===");

    // Include carga la relación — sin esto Ventas viene null
    var empleados = await db.Empleados
        .Include(e => e.Ventas)
        .OrderBy(e => e.Nombre)
        .ToListAsync();

    foreach (var e in empleados)
    {
        var totalVentas = e.Ventas.Sum(v => v.Monto);
        Console.WriteLine($"\n{e.Nombre} ({e.Departamento})");

        if (e.Ventas.Any())
        {
            foreach (var v in e.Ventas.OrderBy(v => v.Fecha))
                Console.WriteLine($"  {v.Fecha:dd/MM/yyyy} — {v.Producto} — ${v.Monto:F0}");
            Console.WriteLine($"  Total vendido: ${totalVentas:F0}");
        }
        else
        {
            Console.WriteLine("  Sin ventas registradas.");
        }
    }
    Console.WriteLine();
}

// ── ESTADÍSTICAS CON LINQ + EF ─────────────────────
async Task MostrarEstadisticasAsync()
{
    Console.WriteLine("=== ESTADÍSTICAS ===");

    // Todo esto se ejecuta como SQL en el servidor
    var stats = await db.Empleados
        .Include(e => e.Ventas)
        .Where(e => e.Ventas.Any())
        .Select(e => new
        {
            e.Nombre,
            e.Departamento,
            TotalVentas = e.Ventas.Sum(v => v.Monto),
            CantidadVentas = e.Ventas.Count()
        })
        .OrderByDescending(e => e.TotalVentas)
        .ToListAsync();

    Console.WriteLine($"\n{"Nombre",-15} {"Depto",-8} {"Total",10} {"Ventas",8}");
    Console.WriteLine(new string('─', 45));

    foreach (var s in stats)
        Console.WriteLine($"{s.Nombre,-15} {s.Departamento,-8} ${s.TotalVentas,9:F0} {s.CantidadVentas,8}");

    var totalGeneral = stats.Sum(s => s.TotalVentas);
    Console.WriteLine(new string('─', 45));
    Console.WriteLine($"{"TOTAL",-15} {"",8} ${totalGeneral,9:F0}");
}

// ── MODELOS ────────────────────────────────────────

public class Empleado
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Departamento { get; set; }
    public decimal Salario { get; set; }
    public DateTime FechaIngreso { get; set; }

    // Relación: un empleado tiene muchas ventas
    public List<Venta> Ventas { get; set; } = new();
}

public class Venta
{
    public int Id { get; set; }
    public decimal Monto { get; set; }
    public string Producto { get; set; }
    public DateTime Fecha { get; set; }

    // Clave foránea
    public int EmpleadoId { get; set; }
    public Empleado Empleado { get; set; }
}

// ── DB CONTEXT ─────────────────────────────────────

public class EmpresaContext : DbContext
{
    public DbSet<Empleado> Empleados { get; set; }
    public DbSet<Venta> Ventas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(
            @"Server=(localdb)\MSSQLLocalDB;Database=EmpresaDB;Trusted_Connection=True;"
        );
}
