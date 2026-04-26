namespace SistemaEmpleados_API.Models;

// Lo que el cliente manda para CREAR un empleado
public class CrearEmpleadoDto
{
    public string Nombre { get; set; }
    public string Departamento { get; set; }
    public decimal Salario { get; set; }
}

// Lo que la API devuelve cuando listás empleados
public class EmpleadoDto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Departamento { get; set; }
    public decimal Salario { get; set; }
    public DateTime FechaIngreso { get; set; }
    public int CantidadVentas { get; set; }
    public decimal TotalVentas { get; set; }
}

// Lo que el cliente manda para ACTUALIZAR el salario
public class ActualizarSalarioDto
{
    public decimal NuevoSalario { get; set; }
}