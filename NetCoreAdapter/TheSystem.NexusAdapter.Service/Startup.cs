using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nexus.Link.Libraries.Web.AspNet.Pipe.Inbound;
using Swashbuckle.AspNetCore.Swagger;
using TheSystem.NexusAdapter.Service.Adapter.Contract;
using TheSystem.NexusAdapter.Service.Adapter.Logic;
using TheSystem.NexusAdapter.Service.Adapter.Logic.OnBoarding;
using TheSystem.NexusAdapter.Service.Mock.CrmSystemMock;
using TheSystem.NexusAdapter.Service.Mock.NexusApiMock;
using TheSystem.NexusAdapter.Service.NexusApi.CapabilityContracts.OnBoarding;
using TheSystem.NexusAdapter.Service.NexusApi.NexusApiContract;
using TheSystem.NexusAdapter.Service.System.CrmSystemContract;

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
            services.AddSingleton<INexusApi, NexusApiMock>();
            services.AddSingleton<IAdapterService, AdapterServiceForSystem>();
            services.AddSingleton<ICrmSystem, CrmSystemMock>();
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
                //app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            // Get the correlation ID from the request header and store it in FulcrumApplication.Context
            app.UseNexusSaveCorrelationId();
            // Start and stop a batch of logs, see also Nexus.Link.Libraries.Core.Logging.BatchLogger.
            app.UseNexusBatchLogs();
            // Log all requests and responses
            app.UseNexusLogRequestAndResponse();
            // Convert exceptions into error responses (HTTP status codes 400 and 500)
            app.UseNexusExceptionToFulcrumResponse();

            app.UseMvc();
        }
    }
}
