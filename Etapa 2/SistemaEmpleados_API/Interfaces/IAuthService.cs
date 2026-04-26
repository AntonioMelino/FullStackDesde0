using SistemaEmpleados_API.Models;

namespace SistemaEmpleados_API.Interfaces;

public interface IAuthService
{
    LoginResponseDto? Login(LoginDto dto);
}