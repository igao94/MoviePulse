﻿using Application.Core;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace API.Middleware;

public class ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, IHostEnvironment env) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException ex)
        {
            await HandleValidationExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleValidationExceptionAsync(HttpContext context, ValidationException ex)
    {
        Dictionary<string, string[]> validationErrors = [];

        if (ex.Errors is not null)
        {
            foreach (var error in ex.Errors)
            {
                if (validationErrors.TryGetValue(error.PropertyName, out var existingErrors))
                {
                    validationErrors[error.PropertyName] = existingErrors.Append(error.ErrorMessage).ToArray();
                }
                else
                {
                    validationErrors[error.PropertyName] = new[] { error.ErrorMessage };
                }
            }
        }

        context.Response.StatusCode = StatusCodes.Status400BadRequest;

        var validationProblemDetails = new ValidationProblemDetails(validationErrors)
        {
            Status = context.Response.StatusCode,
            Type = "ValidationFailed",
            Title = "Validation error",
            Detail = "On or more validation error occurred"
        };

        await context.Response.WriteAsJsonAsync(validationProblemDetails);
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        logger.LogError(ex, ex.Message);

        context.Response.ContentType = "application/json";

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var response = env.IsDevelopment()
            ? new AppException(context.Response.StatusCode, ex.Message, ex.StackTrace)
            : new AppException(context.Response.StatusCode, ex.Message, null);

        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        var json = JsonSerializer.Serialize(response, options);

        await context.Response.WriteAsync(json);
    }
}
