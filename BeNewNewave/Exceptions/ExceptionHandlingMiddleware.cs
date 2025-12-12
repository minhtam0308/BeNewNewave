
using BeNewNewave.DTOs;
using BeNewNewave.Strategy.ResponseDtoStrategy;
using Serilog;
using System.Net;
using System.Text.Json;

namespace BeNewNewave.Exceptions
{


    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private ResponseDto _response = new ResponseDto();

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
                Log.Information("Error Request {t}", ex);

                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            _response.SetResponseDtoStrategy(new ServerError());

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            await context.Response.WriteAsync(JsonSerializer.Serialize(_response.GetResponseDto()));
        }
    }
}
