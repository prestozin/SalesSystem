using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace sales_system.Middlewares;

public class GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException exception)
        {
            logger.LogWarning(exception, "Erro de validação durante a requisição.");

            if (context.Response.HasStarted)
                throw;

            context.Response.Clear();
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            Dictionary<string, string[]> errors = exception.Errors
                .GroupBy(error => error.PropertyName)
                .ToDictionary(group => group.Key, group => group
                    .Select(error => error.ErrorMessage)
                    .ToArray());

            ValidationProblemDetails problem = new ValidationProblemDetails(errors)
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Um ou mais erros de validação ocorreram."
            };

            await context.Response.WriteAsJsonAsync(problem);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ocorreu um erro inesperado durante a requisição.");

            if (context.Response.HasStarted)
                throw;

            context.Response.Clear();
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            ProblemDetails problem = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Erro interno do servidor.",
                Detail = "Não foi possível processar a solicitação."
            };

            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}