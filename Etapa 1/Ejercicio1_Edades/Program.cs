Console.Write("Ingrese su edad: ");
int edad = int.Parse(Console.ReadLine());
if (edad < 13)
{
    Console.WriteLine("Niño");
    int dieciocho = 18 - edad;
    Console.WriteLine($"Para los 18 faltan {dieciocho} años");
}else if(edad >= 13 && edad <= 17)
{
    Console.WriteLine("Adolescente");
    int dieciocho = 18 - edad;
    Console.WriteLine($"Para los 18 faltan {dieciocho} años");
}
else if(edad >= 18 &&  edad <= 64)
{
    Console.WriteLine("Adulto");
    int dieciocho = edad - 18;
    Console.WriteLine($"Para los 18 pasaron {dieciocho} años");
}
else
{
    Console.WriteLine("Adulto mayor");
    int dieciocho = edad - 18;
    Console.WriteLine($"Para los 18 pasaron {dieciocho} años");
}

