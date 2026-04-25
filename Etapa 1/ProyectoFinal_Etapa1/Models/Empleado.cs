using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal_Etapa1.Models
{
    public class Empleado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Departamento { get; set; }
        public decimal Salario { get; set; }
        public DateTime FechaIngreso { get; set; }
        public List<Venta> Ventas { get; set; } = new();
    }

}
