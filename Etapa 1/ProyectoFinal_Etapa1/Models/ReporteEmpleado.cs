using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal_Etapa1.Models;

public class ReporteEmpleado
{
    public string Nombre { get; set; }
    public string Departamento { get; set; }
    public decimal Salario { get; set; }
    public decimal TotalVentas { get; set; }
    public int CantidadVentas { get; set; }
    public int AnosEnEmpresa { get; set; }
}
