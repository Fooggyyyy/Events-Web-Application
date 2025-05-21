using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "Произошло необработанное исключение при обработке запроса {Method} {Url}",
            context.Request.Method,
            context.Request.Path);

        var (statusCode, title) = GetErrorDetails(exception);

        var response = new ProblemDetails
        {
            Title = title,
            Status = statusCode,
            Detail = exception.Message,
            Instance = context.Request.Path
        };

        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = statusCode;

        await context.Response.WriteAsJsonAsync(response);
    }

    private (int statusCode, string title) GetErrorDetails(Exception exception)
    {
        return exception switch
        {
            UnauthorizedAccessException ex when ex.Message.Contains("email") =>
                (StatusCodes.Status401Unauthorized, "Ошибка авторизации: Пользователь с таким email не найден"),

            UnauthorizedAccessException _ =>
                (StatusCodes.Status401Unauthorized, "Ошибка авторизации: Доступ запрещен"),

            _ => (StatusCodes.Status500InternalServerError, "Критическая ошибка сервера")
        };
    }
}

