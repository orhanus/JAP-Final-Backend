using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _env = env;
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                await LogEventAndCreateResponseAsync(context, ex);
            }
        }

        #region Private Methods
        /// <summary>
        /// Asseses the severity of a thrown exception and logs accordingly
        /// For expected exceptions logging level is information
        /// For unexpected exception logging level is error
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        private async Task LogEventAndCreateResponseAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            ApiException response;

            if (ex is UnauthorizedAccessException || ex is ArgumentException)
            {
                context.Response.StatusCode = ex is ArgumentException ? 
                    (int)HttpStatusCode.BadRequest : (int)HttpStatusCode.Unauthorized;

                _logger.LogInformation(ex, ex.Message);

                response = new ApiException(context.Response.StatusCode, ex.Message);
            }
            else
            {
                _logger.LogError(ex, ex.Message);

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                response = _env.IsDevelopment() ?
                    new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString()) :
                    new ApiException(context.Response.StatusCode, "Internal Server Error");
            }

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var json = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(json);
        }
        #endregion
    }
}