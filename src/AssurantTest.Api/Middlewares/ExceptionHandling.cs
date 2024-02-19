using AssurantTest.Domain.Models.Exception;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

namespace AssurantTest.Api.Middlewares
{
    public static class ExceptionHandling
    {
        public static void GlobalExceptionHandler(this WebApplication app)
        {
            _ = app.UseExceptionHandler(options =>
            {
                options.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    var ex = context.Features.Get<IExceptionHandlerFeature>();
                    if (ex != null)
                    {
                        var logger = app.Services.GetService<ILogger<Program>>();
                        logger!.LogError(ex.Error.Message);
                        await context.Response.WriteAsync(JsonSerializer.Serialize(new ErrorResponse("Something Went wrong!!")));
                    }
                });
            });
        }
    }
}
