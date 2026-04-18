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
            Console.WriteLine("Ingrese la temperatura: ");
            break;

        case "2":
            Console.WriteLine("Ingrese el peso en kg: ");
            break;

        case "3":
            Console.WriteLine("Ingrese el numero: ");
            break;

        case "4":
            Console.WriteLine("Adios");
            continuar= false;
            break;

        default:
            Console.WriteLine("Ingrese la temperatura: ");
            break;
    }
}