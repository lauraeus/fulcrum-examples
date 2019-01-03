using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Frobozz.GdprConsent.NexusAdapter.WebApp.Libraries.Web.AspNet.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Nexus.Link.Libraries.Core.Assert;
using Nexus.Link.Libraries.Core.Logging;
using Nexus.Link.Libraries.Web.AspNet.Error.Logic;

namespace Frobozz.GdprConsent.NexusAdapter.WebApp.Libraries.Web.AspNet.Middleware
{
    public class ConvertExceptionToFulcrumResponse
    {
        private readonly RequestDelegate _next;

        /// <inheritdoc />
        public ConvertExceptionToFulcrumResponse(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                Log.LogError($"The web service had an internal exception ({exception.Message})", exception);

                var response = AspNetExceptionConverter.ToContentResult(exception);
                Log.LogInformation(
                    $"Exception ({exception.Message}) was converted to an HTTP response ({response.StatusCode}).");

                FulcrumAssert.IsTrue(response.StatusCode.HasValue);
                Debug.Assert(response.StatusCode.HasValue);
                context.Response.StatusCode = response.StatusCode.Value;
                context.Response.ContentType = response.ContentType;
                await context.Response.WriteAsync(response.Content);
            }
        }
    }

    public static class ConvertExceptionToFulcrumResponseExtension
    {
        public static IApplicationBuilder UseNexusConvertExceptionToFulcrumResponse(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ConvertExceptionToFulcrumResponse>();
        }
    }
}