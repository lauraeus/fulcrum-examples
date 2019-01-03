using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Frobozz.GdprConsent.NexusAdapter.WebApp.Libraries.Web.AspNet.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Nexus.Link.Libraries.Core.Logging;

namespace Frobozz.GdprConsent.NexusAdapter.WebApp.Libraries.Web.AspNet.Middleware
{
    public class LogRequestAndResponse
    {
        private readonly RequestDelegate _next;

        /// <inheritdoc />
        public LogRequestAndResponse(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var timer = new Stopwatch();
            timer.Start();
            try
            {
                // Call the next delegate/middleware in the pipeline
                await _next(context);
                timer.Stop();
                LogResponse(context.Request, context.Response, timer.Elapsed);
            }
            catch (Exception e)
            {
                timer.Stop();
                LogException(context.Request, e, timer.Elapsed);
                throw;
            }
        }

        private static void LogResponse(HttpRequest request, HttpResponse response, TimeSpan elapsedTime)
        {
            if (response == null) return;
            Log.LogInformation($"INBOUND request-response {request.ToLogString(response, elapsedTime)}");
        }

        private static void LogException(HttpRequest request, Exception exception, TimeSpan elapsedTime)
        {
            Log.LogError($"INBOUND request-exception {request.ToLogString(elapsedTime)} | {exception.ToLogString(true)}");
        }
    }
    public static class LogRequestAndResponseExtension
    {
        public static IApplicationBuilder UseNexusLogRequestAndResponse(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogRequestAndResponse>();
        }
    }
}