//Creá un programa con menú de opciones que se repita hasta que el usuario elija "Salir". Las opciones son:
//    1) Convertir temperatura(Celsius a Fahrenheit y viceversa), 
//    2) Calcular IMC(peso y altura), 
//    3) Verificar si un número es primo, 
//    4) Salir. Cada opción debe estar en su propio método.

bool continuar = true;

while (continuar)
{
    Console.WriteLine("MENU PRINCIPAL");
    Console.WriteLine("1 - Convertir temperatura");
    Console.WriteLine("2 - Calcular IMC");
    Console.WriteLine("3 - Verificar si un numero es primo");
    Console.WriteLine("4 - Salir");

    string opcion = Console.ReadLine();

    switch(opcion)
    {
        case "1":
            ConvertirTemperatura();
            break;

        case "2":
            CalcularIMC();
            break;

        case "3":
            EsPrimo();
            break;

        case "4":
            Console.WriteLine("Adios");
            continuar= false;
            break;

        default:
            Console.WriteLine("Opcion no valida, ingrese de nuevo");
            break;
    }
}

void ConvertirTemperatura()
{
    Console.WriteLine("Ingrese la temperatura: ");
    double temperatura = double.Parse(Console.ReadLine());
    Console.WriteLine("Ingrese 1 si es Celsius o 2 si es Fahrenheit: ");
    int tipo = int.Parse(Console.ReadLine());
    if (tipo == 1)
    {
        double resultado = (temperatura * 1.8) + 32;
        Console.WriteLine($"La temperatura de Celsius a Fahrenheit es de: {resultado}");
    }
    else if (tipo == 2)
    {
        double resultado2 = (temperatura - 32) * 0.5556;
        Console.WriteLine($"La temperatura de Fahrenheit a Celsius es de: {resultado2}");
    }
    else
    {
        Console.WriteLine("Opcion invalida");
    }
}

void CalcularIMC()
{
    Console.WriteLine("Ingrese el peso en kg: ");
    double peso = double.Parse(Console.ReadLine());
    Console.WriteLine("Ingrese su altura en metros: ");
    double altura = double.Parse(Console.ReadLine());
    double resultado = peso / (altura * altura);
    Console.WriteLine($"Su IMC es: {resultado}");
}

void EsPrimo()
{
    Console.WriteLine("Ingrese un numero: ");
    int n = int.Parse(Console.ReadLine());
    int contador = 0;
    for (int i = 1; i <= n; i++)
    {
        if (n % i == 0)
        {
            contador++;
        }
    }
    if (contador == 2)
    {
        Console.WriteLine($"El numero {n}, es primo");
    }
    else
    {
        Console.WriteLine($"El numero {n}, NO es primo");
    }
}