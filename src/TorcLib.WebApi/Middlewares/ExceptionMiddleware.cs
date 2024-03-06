using System.Net.Mime;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using TorcLib.Domain.Exceptions;
using TorcLib.IoC.Factories;

namespace TorcLib.WebApi.Middlewares;

public static class ExceptionMiddleware
{
    private const string GenericMessage = "An error has occurred.";

    public static void ConfigureExceptionMiddleware(this IApplicationBuilder app)
    {
        app
            .UseExceptionHandler(exceptionHandlerApp => exceptionHandlerApp.Run(async context =>
            {
                context.Response.ContentType = MediaTypeNames.Text.Plain;

                var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

                context.Response.StatusCode = exception == null
                    ? StatusCodes.Status500InternalServerError
                    : GetStatusCodeByExceptionType(exception);

                var message = context.Response.StatusCode == StatusCodes.Status500InternalServerError
                    ? GenericMessage
                    : exception!.Message;

                ContainerFactory.GetInstance<IDiagnosticContext>()?.SetException(exception);

                await context.Response.WriteAsync(message);
            }));
    }

    private static int GetStatusCodeByExceptionType(Exception exception)
    {
        return exception switch
        {
            LibraryNotFoundException => StatusCodes.Status404NotFound,
            LibraryInvalidOperationException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}