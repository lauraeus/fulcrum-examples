using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Frobozz.GdprConsent.NexusAdapter.WebApp.Libraries.Web.AspNet.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Nexus.Link.Libraries.Core.Context;
using Nexus.Link.Libraries.Core.Logging;
using Nexus.Link.Libraries.Core.MultiTenant.Context;
using Nexus.Link.Libraries.Core.MultiTenant.Model;
using Nexus.Link.Libraries.Core.Platform.Configurations;

namespace Frobozz.GdprConsent.NexusAdapter.WebApp.Libraries.Web.AspNet.Middleware
{
    public class SaveConfiguration
    {
        private readonly RequestDelegate _next;
        private readonly ILeverServiceConfiguration _serviceConfiguration;
        private ConfigurationValueProvider _configurationProvider;
        private CorrelationIdValueProvider _correlationIdValueProvider;
        private TenantValueProvider _tenantProvider;

        /// <inheritdoc />
        public SaveConfiguration(RequestDelegate next, ILeverServiceConfiguration serviceConfiguration)
        {
            _next = next;
            _serviceConfiguration = serviceConfiguration;
            _tenantProvider = new TenantValueProvider();
            _configurationProvider = new ConfigurationValueProvider(); ;
            _correlationIdValueProvider = new CorrelationIdValueProvider();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var rgx = new Regex("/v[^/]+/([^/]+)/([^/]+)/");

            var match = rgx.Match(context.Request.GetEncodedUrl());
            if (match.Success && match.Groups.Count == 3)
            {
                var organization = match.Groups[1].Value;
                var environment = match.Groups[2].Value;

                var tenant = new Tenant(organization, environment);
                _tenantProvider.Tenant = tenant;
                try
                {
                    // SaveConfiguration should be run before SaveCorrelationId, so setup correlation id from request header if necessary
                    if (string.IsNullOrWhiteSpace(_correlationIdValueProvider.CorrelationId))
                    {
                        var correlationId = SaveCorrelationId.ExtractCorrelationIdFromHeader(context.Request);
                        if (!string.IsNullOrWhiteSpace(correlationId))
                            _correlationIdValueProvider.CorrelationId = correlationId;
                    }

                    var configuration = await _serviceConfiguration.GetConfigurationForAsync(tenant);
                    _configurationProvider.LeverConfiguration = configuration;
                }
                catch
                {
                    // Deliberately ignore errors for configuration. This will have to be taken care of when the configuration is needed.
                }
            }

            await _next(context);
        }
    }

    public static class SaveConfigurationExtension
    {
        public static IApplicationBuilder UseNexusSaveConfiguration(
            this IApplicationBuilder builder, ILeverServiceConfiguration serviceConfiguration)
        {
            return builder.UseMiddleware<SaveConfiguration>(serviceConfiguration);
        }
    }
}