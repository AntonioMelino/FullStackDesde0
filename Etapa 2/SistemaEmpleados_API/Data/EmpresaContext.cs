using Microsoft.EntityFrameworkCore;
using SistemaEmpleados_API.Models;

namespace SistemaEmpleados_API.Data;

public class EmpresaContext : DbContext
{
    public EmpresaContext(DbContextOptions<EmpresaContext> options) : base(options) { }

    public DbSet<Empleado> Empleados { get; set; }
    public DbSet<Venta> Ventas { get; set; }
}