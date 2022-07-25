﻿using System.Net;
using System.Text.Json;

namespace FurnitureStore.WebApi.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, 
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error - {e}");
            await HandleExceptionAsync(httpContext, e, HttpStatusCode.NotFound);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext,
        Exception exception, HttpStatusCode statusCode)
    {
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)statusCode;

        var errorDto = new
        {
            Message = exception.Message,
            StatusCode = (int)statusCode,
        };

        string result = JsonSerializer.Serialize(errorDto);

        await httpContext.Response.WriteAsync(result);
    }
}