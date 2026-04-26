using System.ComponentModel.DataAnnotations;

namespace SistemaEmpleados_API.Models;

public class CrearEmpleadoDto
{
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres.")]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "El departamento es obligatorio.")]
    public string Departamento { get; set; }

    [Required(ErrorMessage = "El salario es obligatorio.")]
    [Range(100, 999999, ErrorMessage = "El salario debe estar entre 100 y 999999.")]
    public decimal Salario { get; set; }
}

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

public class ActualizarSalarioDto
{
    [Required]
    [Range(100, 999999, ErrorMessage = "El salario debe estar entre 100 y 999999.")]
    public decimal NuevoSalario { get; set; }
}