namespace PruebaTecnica.Control_Excepciones;

using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        // Establece el código de estado dependiendo del tipo de excepción
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";

        // Crear el objeto de respuesta con el mensaje de error
        var response = new
        {
            StatusCode = context.Response.StatusCode,
            Message = exception.Message,
            Details = exception.InnerException?.Message
        };

        // Serializa la respuesta en formato JSON
        var result = JsonSerializer.Serialize(response);

        return context.Response.WriteAsync(result);
    }
}

