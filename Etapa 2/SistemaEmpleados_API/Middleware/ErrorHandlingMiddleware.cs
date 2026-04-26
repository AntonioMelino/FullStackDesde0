using System.Net;
using System.Text.Json;

namespace SistemaEmpleados_API.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);  // ejecuta el resto del pipeline
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error no manejado: {Message}", ex.Message);
            await ManejarExcepcionAsync(context, ex);
        }
    }

    private static async Task ManejarExcepcionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";

        var (statusCode, mensaje) = ex switch
        {
            KeyNotFoundException => (HttpStatusCode.NotFound, ex.Message),
            ArgumentException => (HttpStatusCode.BadRequest, ex.Message),
            UnauthorizedAccessException => (HttpStatusCode.Unauthorized, ex.Message),
            _ => (HttpStatusCode.InternalServerError, "Ocurrió un error interno.")
        };

        context.Response.StatusCode = (int)statusCode;

        var respuesta = new
        {
            status = (int)statusCode,
            mensaje = mensaje,
            fecha = DateTime.UtcNow
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(respuesta));
    }
}