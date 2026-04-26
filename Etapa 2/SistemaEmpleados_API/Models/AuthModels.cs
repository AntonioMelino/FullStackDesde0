namespace SistemaEmpleados_API.Models;

public class LoginDto
{
    public string Usuario { get; set; }
    public string Password { get; set; }
}

public class LoginResponseDto
{
    public string Token { get; set; }
    public DateTime Expiracion { get; set; }
    public string Usuario { get; set; }
}