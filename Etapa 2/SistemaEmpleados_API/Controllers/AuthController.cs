using Microsoft.AspNetCore.Mvc;
using SistemaEmpleados_API.Interfaces;
using SistemaEmpleados_API.Models;

namespace SistemaEmpleados_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public ActionResult<LoginResponseDto> Login(LoginDto dto)
    {
        var response = _authService.Login(dto);

        if (response == null)
            return Unauthorized("Usuario o contraseña incorrectos.");

        return Ok(response);
    }
}