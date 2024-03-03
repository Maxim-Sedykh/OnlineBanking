using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineBanking.Domain.Result;
using Serilog;
using System.Net;
using ILogger = Serilog.ILogger;

namespace OnlineBanking.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger logger)
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
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            _logger.Error(exception, exception.Message);

            var errorMessage = exception.Message;
            var response = exception switch
            {
                UnauthorizedAccessException => new Result() { ErrorMessage = errorMessage, ErrorCode = (int)HttpStatusCode.Unauthorized },
                _ => new Result() { ErrorMessage = "Internal Server Error", ErrorCode = (int)HttpStatusCode.InternalServerError },
            };

            httpContext.Response.StatusCode = (int)response.ErrorCode;
            httpContext.Response.Redirect($"/home/error/{response.ErrorMessage}");

            return Task.CompletedTask;
        }
    }
}
