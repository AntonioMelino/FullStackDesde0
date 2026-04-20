using System.Security.Cryptography.X509Certificates;

var emp1 = new Empleado("Antonio", "Melino", 1500, new DateTime(2020, 3, 15));
emp1.MostrarInfo();

emp1.AumentarSalario(10);
emp1.MostrarInfo();

emp1.AumentarSalario(-5);
emp1.AumentarSalario(150);

//emp1.Salario = 9999;
public class Empleado
{
    // Propiedades públicas
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public decimal Salario { get; private set; } // solo se modifica desde adentro
    public DateTime FechaIngreso { get; private set; }

    // Campo privado — invisible desde afuera
    private int _intentosFallidos = 0;

    // Constructor
    public Empleado(string nombre, string apellido, decimal salario, DateTime fechaIngreso)
    {
        Nombre = nombre;
        Apellido = apellido;
        Salario = salario;
        FechaIngreso = fechaIngreso;
    }

    // Métodos públicos
    public string NombreCompleto() => $"{Nombre} {Apellido}";

    public void AumentarSalario(decimal porcentaje)
    {
        if(porcentaje <= 0 || porcentaje > 100)
        {
            _intentosFallidos++;
            Console.WriteLine($"Porcentaje invalido. Intentos fallidos: {_intentosFallidos}");
            return;
        }
        Salario = Salario * (1 + porcentaje / 100);
    }
    public int AnosEnEmpresa()
    {
        TimeSpan diferencia = DateTime.Now - FechaIngreso;
        return (int)(diferencia.TotalDays / 365.25);
    }

    public void MostrarInfo()
    {
        Console.WriteLine($"Empleado: {NombreCompleto()} | Salario: ${Salario:F2} | Años en la empresa: {AnosEnEmpresa()}");
    }

}
