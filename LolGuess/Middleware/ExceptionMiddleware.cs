using API.Errors;
using System.Net;
using System.Text.Json;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }
        public async Task InvokeAsync(HttpContext ctx)
        {
            try
            {
                await _next(ctx);
            }
            catch (Exception ex)
            {
                var code = (int)HttpStatusCode.InternalServerError;

                _logger.LogError(ex, ex.Message);
                ctx.Response.ContentType = "application/json";
                ctx.Response.StatusCode = code;

                var response = _env.IsDevelopment()
                    ? new ApiException(code, ex.Message, ex.StackTrace.ToString())
                    : new ApiException(code);

                var opt = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var json = JsonSerializer.Serialize(response, opt);

                await ctx.Response.WriteAsync(json);
            }
        }
    }
}
