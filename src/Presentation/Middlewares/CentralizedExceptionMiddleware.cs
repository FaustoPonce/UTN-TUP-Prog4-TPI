using Domain.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Text.Json;

namespace Presentation.Middlewares
{

    public class CentralizedExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<CentralizedExceptionMiddleware> _logger;

        public CentralizedExceptionMiddleware(ILogger<CentralizedExceptionMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context); //  llama al metodo del siguiente middleware en el pipeline
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";

                switch (ex)
                {
                    // Seteamos el codigo de estado y log correcto en cada caso
                    case NotFoundException notFoundEx:
                        _logger.LogWarning(notFoundEx, "Not Found Exception");
                        context.Response.StatusCode = StatusCodes.Status404NotFound;
                        break;
                    case ValidationException validationEx:
                        _logger.LogWarning(validationEx, "Validation Exception");
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        break;
                    case UnauthorizedAccessException unauthorizedEx:
                        _logger.LogWarning(unauthorizedEx, "Unauthorized Exception");
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        break;
                    default:
                        _logger.LogError(ex, "Undhandled Exception");
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        break;
                }
                
                // agregamos el mensaje de error que tiramos con la excepcion y escribimos la respuesta
                var response = new { error = ex.Message };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
