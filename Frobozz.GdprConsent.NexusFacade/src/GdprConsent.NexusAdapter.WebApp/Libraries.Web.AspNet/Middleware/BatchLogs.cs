using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Frobozz.GdprConsent.NexusAdapter.WebApp.Libraries.Web.AspNet.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Nexus.Link.Libraries.Core.Logging;

namespace Frobozz.GdprConsent.NexusAdapter.WebApp.Libraries.Web.AspNet.Middleware
{
    public class BatchLogs
    {
        private readonly RequestDelegate _next;

        /// <inheritdoc />
        public BatchLogs(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Log.StartBatch();
            try
            {
                await _next(context);
            }
            finally
            {
                Log.ExecuteBatch();
            }
        }
    }
    public static class BatchLogsExtension
    {
        public static IApplicationBuilder UseNexusBatchLogs(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BatchLogs>();
        }
    }
}