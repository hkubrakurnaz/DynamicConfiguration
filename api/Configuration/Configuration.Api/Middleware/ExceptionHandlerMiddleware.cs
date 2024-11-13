using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Configuration.Api.Models.Responses.Base;
using Configuration.Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Configuration.Api.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
    
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                httpContext.Request.EnableBuffering();
                await _next.Invoke(httpContext);
            }
            catch (Exception e)
            {
                await HandleAsync(e, httpContext);
            }
        }
        
        private static async Task HandleAsync(Exception exception, HttpContext context)
        {
            var statusCode = DecideStatusCode(exception);
            var payload = DecidePayload(exception);
    
            var errorHttpContentStr = JsonSerializer.Serialize(payload);
    
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(errorHttpContentStr);
        }
        
        private static HttpStatusCode DecideStatusCode(Exception exception)
        {
            return exception switch
            {
                NotFoundException => HttpStatusCode.NotFound,
                ValidationException => HttpStatusCode.BadRequest,
                ConflictException => HttpStatusCode.Conflict,
                _ => HttpStatusCode.InternalServerError
            };
        }
        
        private static object DecidePayload(Exception exception)
        {
            object payload = exception switch
            {
                ValidationException validationException => new ValidationErrorResponse(validationException),
                CustomException customException => new ErrorResponse(customException.Message, customException.Object),
                _ => new { Message = "INTERNAL SERVER ERROR" }
            };
    
            return payload;
        }
    }
}

