using Microsoft.EntityFrameworkCore;
using ProyectoFinal_Etapa1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal_Etapa1.Data;

public class EmpresaContext : DbContext
{
    public DbSet<Empleado> Empleados { get; set; }
    public DbSet<Venta> Ventas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(
            @"Server=(localdb)\MSSQLLocalDB;Database=EmpresaFinalDB;Trusted_Connection=True;"
        );
}