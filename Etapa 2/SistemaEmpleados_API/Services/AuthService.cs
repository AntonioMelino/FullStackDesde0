using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SistemaEmpleados_API.Interfaces;
using SistemaEmpleados_API.Models;

namespace SistemaEmpleados_API.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _config;

    public AuthService(IConfiguration config)
    {
        _config = config;
    }

    public LoginResponseDto? Login(LoginDto dto)
    {
        // Usuarios hardcodeados por ahora — en producción vendrían de la BD
        var usuariosValidos = new Dictionary<string, string>
        {
            { "admin", "admin123" },
            { "antonio", "password123" }
        };

        if (!usuariosValidos.TryGetValue(dto.Usuario, out string? passwordCorrecta))
            return null;

        if (passwordCorrecta != dto.Password)
            return null;

        return GenerarToken(dto.Usuario);
    }

    private LoginResponseDto GenerarToken(string usuario)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiracion = DateTime.UtcNow.AddHours(double.Parse(_config["Jwt:ExpirationHours"]!));

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, usuario),
            new Claim(ClaimTypes.Role, usuario == "admin" ? "Admin" : "User"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: expiracion,
            signingCredentials: creds
        );

        return new LoginResponseDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiracion = expiracion,
            Usuario = usuario
        };
    }
}