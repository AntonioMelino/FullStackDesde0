var gerente = new Gerente("Laura", "Gomez", 3000, new DateTime(2018, 1, 10), 8);
var dev1 = new Desarrollador("Antonio", "Melino", 1500, new DateTime(2023, 6, 1), "C#", "Junior");
var dev2 = new Desarrollador("Sofia", "Paz", 2000, new DateTime(2020, 3, 15), "React", "Semi");
var pasante = new Pasante("Enzo", "Oliva", 1000, new DateTime(2026, 1, 1), "UTN");

var equipo = new List<Empleado> { gerente, dev1, dev2, pasante };

Console.WriteLine("=== EQUIPO COMPLETO ===");
foreach (var emp in equipo)
    emp.MostrarInfo();

Console.WriteLine("\n=== REPORTES ===");
foreach (IReporteable emp in equipo)   // ← tratamos a todos como IReporteable
    Console.WriteLine(emp.GenerarReporte());

Console.WriteLine("\n=== ESTADÍSTICAS ===");
Console.WriteLine($"Salario promedio: ${equipo.Average(e => e.Salario):F2}");
Console.WriteLine($"Mejor pago: {equipo.MaxBy(e => e.Salario).NombreCompleto()}");
Console.WriteLine($"Más antiguo: {equipo.MinBy(e => e.FechaIngreso).NombreCompleto()}");

Console.WriteLine("\n=== SOLO DESARROLLADORES ===");
equipo.OfType<Desarrollador>()
      .ToList()
      .ForEach(d => Console.WriteLine($"{d.NombreCompleto()} — {d.Seniority} en {d.Lenguaje}"));

Console.WriteLine("\n=== BONUSES ===");
foreach (ICalculadoraBonus emp in equipo.OfType<ICalculadoraBonus>())
    Console.WriteLine($"Bonus: ${emp.CalcularBonus():F2}");


// ── INTERFACES ─────────────────────────────────────
public interface IEmpleado
{
    string NombreCompleto();
    void MostrarInfo();
    void AumentarSalario(decimal porcentaje);
}

public interface IReporteable
{
    string GenerarReporte();
}

public interface ICalculadoraBonus
{
    decimal CalcularBonus();
}

public class Empleado : IEmpleado, IReporteable
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

    public virtual void AumentarSalario(decimal porcentaje)
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

    public string GenerarReporte()
    {
        return $"[REPORTE] {NombreCompleto()} | " +
           $"Salario: ${Salario:F2} | " +
           $"Antigüedad: {AnosEnEmpresa()} años | " +
           $"Ingreso: {FechaIngreso:dd/MM/yyyy}";
    }

}

public class Gerente : Empleado, ICalculadoraBonus
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

    public decimal CalcularBonus()
    {
        return Salario * 0.20m;
    }
}

public class Desarrollador : Empleado, ICalculadoraBonus
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

    public decimal CalcularBonus()
    {
        return Seniority switch
        {
            "Junior" => Salario * 0.05m,
            "Semi" => Salario * 0.10m,
            "Senior" => Salario * 0.15m,
            _ => 0m
        };
    }
}


public class Pasante : Empleado
{
    public string Universidad {  get; set; }

    public Pasante(string nombre, string apellido, decimal salario, DateTime fechaIngreso, string universidad) : base(nombre, apellido, salario, fechaIngreso)
    {
        Universidad = universidad;
    }

    public override void MostrarInfo()
    {
        base.MostrarInfo();
        Console.WriteLine($"Estudio en {Universidad}");
    }

    public override void AumentarSalario(decimal porcentaje)
    {
        if(porcentaje > 5)
        {
            Console.WriteLine($"Porcentaje limitado automáticamente de {porcentaje}% a 5%");
            porcentaje = 5;
        }
        base.AumentarSalario(porcentaje);
    }
}