namespace SistemaEmpleados_API.Models;

public class Empleado
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Departamento { get; set; }
    public decimal Salario { get; set; }
    public DateTime FechaIngreso { get; set; }
    public List<Venta> Ventas { get; set; } = new();
}