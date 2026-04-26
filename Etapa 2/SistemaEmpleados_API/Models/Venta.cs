namespace SistemaEmpleados_API.Models;

public class Venta
{
    public int Id { get; set; }
    public string Producto { get; set; }
    public decimal Monto { get; set; }
    public DateTime Fecha { get; set; }
    public int EmpleadoId { get; set; }
    public Empleado Empleado { get; set; }
}