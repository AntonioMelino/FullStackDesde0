using System.Security.Cryptography.X509Certificates;

var gerente = new Gerente("Laura", "Gomez", 3000, new DateTime(2018, 1, 10), 8);
var dev1 = new Desarrollador("Antonio", "Melino", 1500, new DateTime(2023, 6, 1), "C#", "Junior");
var dev2 = new Desarrollador("Sofia", "Paz", 2000, new DateTime(2020, 3, 15), "React", "Semi");

// Los tres son Empleados — podés meterlos en la misma lista
var equipo = new List<Empleado> { gerente, dev1, dev2 };

Console.WriteLine("=== EQUIPO ===");
foreach (var emp in equipo)
    emp.MostrarInfo();  // cada uno ejecuta SU versión de MostrarInfo

Console.WriteLine("\n=== ESTADÍSTICAS ===");
Console.WriteLine($"Salario promedio: ${equipo.Average(e => e.Salario):F2}");
Console.WriteLine($"Empleado mejor pago: {equipo.MaxBy(e => e.Salario).NombreCompleto()}");
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

    public virtual void MostrarInfo()
    {
        Console.WriteLine($"Empleado: {NombreCompleto()} | Salario: ${Salario:F2} | Años en la empresa: {AnosEnEmpresa()}");
    }

}

public class Gerente : Empleado
{
    public int CantidadEquipo { get; set; }
    public decimal Bonus { get; private set; }

    public Gerente(string nombre, string apellido, decimal salario, DateTime fechaIngreso, int cantidadEquipo) : base(nombre, apellido, salario, fechaIngreso) // llama al constructor de Empleado
    {
        CantidadEquipo = cantidadEquipo;
        Bonus = salario * 0.20m; //bonus del 20%
    }

    // Sobreescribe MostrarInfo() de Empleado
    public override void MostrarInfo()
    {
        base.MostrarInfo(); // ejecuta el MostrarInfo original
        Console.WriteLine($" → Gerente de equipo de {CantidadEquipo} personas | Bonus ${Bonus:F2}");
    }
}

public class Desarrollador : Empleado
{
    public string Lenguaje { get; set; }
    public string Seniority { get; set; } // "Junior", "Semi", "Senior"

    public Desarrollador(string nombre, string apellido, decimal salario, DateTime fechaIngreso, string lenguaje, string seniority) : base(nombre, apellido, salario, fechaIngreso)
    {
        Lenguaje = lenguaje;
        Seniority = seniority;
    }

    public override void MostrarInfo()
    {
        base.MostrarInfo();
        Console.WriteLine($" → {Seniority} Developer en {Lenguaje}");
    }
}