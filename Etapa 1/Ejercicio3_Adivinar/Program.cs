//El programa genera un número aleatorio entre 1 y 100. El usuario tiene que adivinarlo.
//En cada intento, el programa dice si el número real es mayor o menor al ingresado.
//Al final, mostrá cuántos intentos necesitó y una calificación: 1–3 intentos = "Genio", 4–7 = "Bien", 8 + = "Sigue practicando".

var rng = new Random();
int secreto = rng.Next(1, 101);
int numero = 0;
int intentos = 0;

Console.WriteLine("Adivina el numero secreto (1-100): ");

while (secreto != numero)
{
    numero = int.Parse(Console.ReadLine());
    intentos++;

    if (secreto < numero)
    {
        Console.WriteLine("No adivinaste, el numero secreto es MENOR al numero elegido, elegi otro numero: ");
    }
    else if(secreto > numero)
    {
        Console.WriteLine("No adivinaste, el numero secreto es MAYOR al numero elegido, elegi otro numero: ");
    }
    
}

Console.WriteLine($"El numero secreto es: {secreto}");
Console.WriteLine($"Intentaste {intentos} veces");

if (intentos <= 3)
{
    Console.WriteLine("Genio");
}
else if (intentos <= 7)
{
    Console.WriteLine("Bien");
}
else
{
    Console.WriteLine("Sigue practicando");
}