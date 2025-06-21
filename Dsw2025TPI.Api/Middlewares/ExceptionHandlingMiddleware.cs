using Dsw2025TPI.Domain.Exceptions;
using System.Text.Json;

namespace Dsw2025TPI.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DuplicatedEntityException ex)
            {
                _logger.LogWarning(ex, "Entidad duplicada");
                await WriteErrorResponse(context, 409, ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogInformation(ex, "Entidad no encontrada");
                await WriteErrorResponse(context, 404, ex.Message);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Argumento inválido");
                await WriteErrorResponse(context, 400, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado");
                await WriteErrorResponse(context, 500, "Error interno del servidor");
            }
        }

        private static async Task WriteErrorResponse(HttpContext context, int statusCode, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(JsonSerializer.Serialize(new { message }));
        }
    }

}
