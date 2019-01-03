using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Frobozz.GdprConsent.NexusAdapter.WebApp.Libraries.Web.AspNet.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Nexus.Link.Libraries.Core.Assert;
using Nexus.Link.Libraries.Core.Context;
using Nexus.Link.Libraries.Core.Logging;
using Nexus.Link.Libraries.Web.Pipe;

namespace Frobozz.GdprConsent.NexusAdapter.WebApp.Libraries.Web.AspNet.Middleware
{
    public class SaveCorrelationId
    {
        private readonly RequestDelegate _next;
        private readonly CorrelationIdValueProvider _correlationIdValueProvider;

        /// <inheritdoc />
        public SaveCorrelationId(RequestDelegate next)
        {
            _next = next;
            _correlationIdValueProvider = new CorrelationIdValueProvider();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            SaveCorrelationIdToExecutionContext(context.Request);
            await _next(context);
        }

        /// <summary>
        /// Read the correlation id header from the <paramref name="request"/> and save it to the execution context.
        /// </summary>
        /// <param name="request">The request that we will read the header from.</param>
        /// <returns></returns>
        /// <remarks>This method is made public for testing purposes. Should normally not be called from outside this class.</remarks>
        public void SaveCorrelationIdToExecutionContext(HttpRequest request)
        {
            InternalContract.RequireNotNull(request, nameof(request));
            var correlationId = ExtractCorrelationIdFromHeader(request);
            var createCorrelationId = string.IsNullOrWhiteSpace(correlationId);
            if (createCorrelationId) correlationId = Guid.NewGuid().ToString();
            FulcrumAssert.IsNotNull(_correlationIdValueProvider);
            _correlationIdValueProvider.CorrelationId = correlationId;
            if (createCorrelationId)
            {
                Log.LogInformation($"Created correlation id {correlationId}, as incoming request did not have it. ({request.ToLogString()})");
            }
        }

        internal static string ExtractCorrelationIdFromHeader(HttpRequest request)
        {
            var correlationHeaderValueExists =
                request.Headers.TryGetValue(Constants.FulcrumCorrelationIdHeaderName, out var correlationIds);
            if (!correlationHeaderValueExists) return null;
            var correlationsArray = correlationIds.ToArray();
            if (correlationsArray.Length > 1)
            {
                // ReSharper disable once UnusedVariable
                var message =
                    $"There was more than one correlation id in the header: {string.Join(", ", correlationsArray)}. The first one was picked as the Fulcrum correlation id from here on.";
                Log.LogWarning(message);
            }
            return correlationsArray[0];
        }
    }
    public static class SaveCorrelationIdExtension
    {
        public static IApplicationBuilder UseNexusSaveCorrelationId(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SaveCorrelationId>();
        }
    }
}