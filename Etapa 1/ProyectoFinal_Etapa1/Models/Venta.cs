using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal_Etapa1.Models;

public class Venta
{
    public int Id { get; set; }
    public string Producto { get; set; }
    public decimal Monto { get; set; }
    public DateTime Fecha { get; set; }
    public int EmpleadoId { get; set; }
    public Empleado Empleado { get; set; }
}