using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cw7.Services;

namespace Cw7.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext, ILoggingService logger)
        {
            httpContext.Request.EnableBuffering();
            if (httpContext.Request != null)
            {
                string method = httpContext.Request.Method;
                string path = httpContext.Request.Path;
                string query = httpContext.Request.QueryString.ToString();
                string bodyStr = "";

                using (var reader = new StreamReader(httpContext.Request.Body, Encoding.UTF8, true, 1024, true))
                {
                    bodyStr = await reader.ReadToEndAsync();
                    httpContext.Request.Body.Position = 0;
                }
                logger.Log(method, path, query, bodyStr);
            }
            if (_next != null) await _next(httpContext);
        }
    }
}
