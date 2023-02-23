using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Extensions
{
    public class ExceptionHandling
    {
        private readonly RequestDelegate _hasNext;

        public ExceptionHandling(RequestDelegate hasNext)
        {
            _hasNext = hasNext;
        }

        public async Task Invoke(HttpContext context)
        {
            context = context ?? throw new ArgumentNullException(nameof(context));

            try
            {
                await _hasNext.Invoke(context);
            }
            catch (Exception ex)
            {
                switch(ex)
                {
                    case UnauthorizedAccessException error:
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        await context.Response.WriteAsync(JsonSerializer.Serialize($"Authentication failed"));
                        break;
                    case ArgumentNullException error:
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        await context.Response.WriteAsync(JsonSerializer.Serialize($"Argument {error.ParamName} cannot be null."));
                        break;
                    case InvalidOperationException error:
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        await context.Response.WriteAsync(JsonSerializer.Serialize($"Invalid operation: {error.Message}"));
                        break;
                    case KeyNotFoundException error:
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        await context.Response.WriteAsync(JsonSerializer.Serialize($"Page not found: {error.Message}"));
                        break;
                    default:
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        await context.Response.WriteAsync(JsonSerializer.Serialize($"An unexpected error occured."));
                        break;
                }
            }
        }
    }
}
