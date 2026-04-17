//Pedí al usuario un número entre 1 y 10. Mostrá su tabla de multiplicar del 1 al 10 con formato prolijo (los números alineados en columnas).
//Después mostrá la suma total de todos los resultados de esa tabla.

Console.Write("Ingresa un numero del 1 al 10: ");
int numero = int.Parse(Console.ReadLine());
int total = 0;
for (int i = 1; i <= 10; i++)
{
    Console.WriteLine($"|{numero}*{i} = {numero * i}| ");
    total = (numero * i)+total;
    
}
Console.WriteLine($"El total es: {total}");
