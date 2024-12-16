using System.ComponentModel.DataAnnotations;
using System.Net;

namespace BankClientstMicroservice.Exceptions
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
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

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            if (exception is ValidationException validationException)
            {
                var result = new
                {
                    error = "Validation error",
                    message = validationException.Message
                };

                return context.Response.WriteAsJsonAsync(result);
            }
            else
            {
                var result = new
                {
                    error = "Ha ocurrido un error inesperado.",
                    message = "Contactarse con soporte."
                };

                return context.Response.WriteAsJsonAsync(result);
            }
        }
    }
}
