//Creá una clase Estudiante con nombre, apellido y una lista de notas (decimales).
//El programa permite: agregar estudiantes, registrar notas para un estudiante,
//mostrar el promedio de cada uno, el alumno con mejor promedio, y cuántos aprobaron (promedio mayor a 6).
//Todo desde un menú de consola.


using System.Diagnostics.CodeAnalysis;

List<Estudiante> estudiantes= new List<Estudiante>();

bool continuar = true;


while (continuar)
{
    Console.Clear();
    Console.WriteLine("1- Agregar estudiantes");
    Console.WriteLine("2- Registrar notas para un estudiante");
    Console.WriteLine("3- Mostrar el promedio de cada estudiante");
    Console.WriteLine("4- Mostrar el alumno con mejor promedio");
    Console.WriteLine("5- Mostrar cuantos aprobaron");
    Console.WriteLine("6- Salir");

    string opcion = Console.ReadLine();

    switch (opcion)
    {
        case "1":
            AgregarEstudiante();
            break;

        case "2":
            AgregarNotas();
            break;

        case "3":
            MostrarPromedio();
            break;

        case "4":
            MostrarMejorPromedio();
            break;

        case "5":
            MostrarAprobados();
            break;

        case "6":
            Console.WriteLine("Adios");
            continuar = false;
            break;

        default:
            Console.WriteLine("Opcion no valida, ingrese de nuevo");
            break;
    }
}

void AgregarEstudiante()
{
    Console.WriteLine("Ingrese el nombre: ");
    string nombre = Console.ReadLine();
    Console.WriteLine("Ingrese el apellido: ");
    string apellido = Console.ReadLine();

    Estudiante e = new Estudiante();
    e.nombre = nombre;
    e.apellido = apellido;
    estudiantes.Add(e);
}

void AgregarNotas()
{
    Console.WriteLine("Ingrese el nro de estudiante al que ingresar la nota");
    for (int i = 0; i < estudiantes.Count; i++)
    {
            Console.WriteLine($"{i}- {estudiantes[i].nombre} {estudiantes[i].apellido}");
    }
    int nroEstudiante = int.Parse(Console.ReadLine());

    string seguir = "s";
    while(seguir == "s")
    { 
    Console.WriteLine("Ingrese las notas: ");
    decimal notas = decimal.Parse(Console.ReadLine());
    estudiantes[nroEstudiante].notas.Add(notas);
    Console.WriteLine("¿Agregar otra nota? (s/n): ");
    seguir = Console.ReadLine();
    }
}

void MostrarPromedio()
{

    for (int i = 0; i < estudiantes.Count; i++)
    {
        decimal suma = 0;
        foreach(decimal nota in estudiantes[i].notas)
        {
            suma += nota;
        }
        decimal promedio = suma / estudiantes[i].notas.Count;
        Console.WriteLine($"{i}- {estudiantes[i].nombre} {estudiantes[i].apellido} - Promedio {promedio}");
    }
    Console.WriteLine("\nPresione cualquier tecla para continuar...");
    Console.ReadKey();
}

void MostrarMejorPromedio()
{
    Estudiante mejor = estudiantes[0];
    decimal mejorPromedio = 0;

    for (int i = 0; i < estudiantes.Count; i++)
    {
        decimal suma = 0;
        foreach (decimal nota in estudiantes[i].notas)
            suma += nota;
        decimal promedio = suma / estudiantes[i].notas.Count;

        if (promedio > mejorPromedio)
        {
            mejorPromedio = promedio;
            mejor = estudiantes[i];
        }
    }
    Console.WriteLine($"Mejor promedio: {mejor.nombre} {mejor.apellido} - {mejorPromedio}");
    Console.WriteLine("\nPresione cualquier tecla para continuar...");
    Console.ReadKey();
}

void MostrarAprobados()
{
    for (int i = 0; i < estudiantes.Count; i++)
    {
        decimal suma = 0;
        foreach (decimal nota in estudiantes[i].notas)
        {
            suma += nota;
        }
        decimal promedio = suma / estudiantes[i].notas.Count;
        if (promedio > 6)
        { 
        Console.WriteLine($"{i}- {estudiantes[i].nombre} {estudiantes[i].apellido} - Promedio {promedio}, APROBADO");
        }
    }
    Console.WriteLine("\nPresione cualquier tecla para continuar...");
    Console.ReadKey();
}

public class Estudiante
{
    public string nombre;
    public string apellido;
    public List<decimal> notas = new List<decimal>();
}
