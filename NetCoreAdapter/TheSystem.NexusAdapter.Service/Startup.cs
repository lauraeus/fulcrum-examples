using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nexus.Link.Libraries.Web.AspNet.Pipe.Inbound;
using Swashbuckle.AspNetCore.Swagger;
using TheSystem.NexusAdapter.Service.CapabilityContracts.OnBoarding;
using TheSystem.NexusAdapter.Service.CrmSystemContract;
using TheSystem.NexusAdapter.Service.CrmSystemMock;
using TheSystem.NexusAdapter.Service.Logic;

namespace TheSystem.NexusAdapter.Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<ICrmSystem>(provider => new CrmSystem());
            services.AddScoped<IOnBoardingService, OnBoardingLogic>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "TheSystem.NexusAdapter", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = "TheSystem.NexusAdapter.Service.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TheSystem.NexusAdapter V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            // Get the correlation ID from the request header and store it in FulcrumApplication.Context
            app.UseNexusSaveCorrelationId();
            // Start and stop a batch of logs, see also Nexus.Link.Libraries.Core.Logging.BatchLogger.
            app.UseNexusBatchLogs();
            // Log all requests and responses
            app.UseNexusLogRequestAndResponse();
            // Convert exceptions into error responses (HTTP status codes 400 and 500)
            app.UseNexusConvertExceptionToFulcrumResponse();
        }
    }
}
