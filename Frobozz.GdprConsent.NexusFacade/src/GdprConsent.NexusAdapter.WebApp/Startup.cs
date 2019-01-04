using System;
using System.IO;
using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.GdprConsent.Logic.Dal.Contracts;
using Frobozz.GdprConsent.Logic.Dal.Mock;
using Frobozz.GdprConsent.Logic.Dal.SqlServer;
using Frobozz.GdprConsent.Logic.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nexus.Link.Libraries.Web.AspNet.Pipe.Inbound;
using Swashbuckle.AspNetCore.Swagger;

namespace Frobozz.GdprConsent.NexusAdapter.WebApp
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

            // Register the Swagger generator, defining 1 or more Swagger documents
            var currentAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            var assemblyName = currentAssembly.GetName().Name;
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = assemblyName, Version = "v1" });

                //c.DocInclusionPredicate((_, api) => !string.IsNullOrWhiteSpace(api.GroupName));
                c.TagActionsBy(api => new [] {api.GroupName ?? api.HttpMethod});

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{assemblyName}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddSingleton<IGdprCapability>(CreateGdprCapability(true));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            var currentAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            var assemblyName = currentAssembly.GetName().Name;
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{assemblyName} V1");
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
            //app.UseNexusSaveConfiguration(serviceConfiguration);
            app.UseNexusSaveCorrelationId();
            app.UseNexusBatchLogs();
            app.UseNexusLogRequestAndResponse();
            app.UseNexusConvertExceptionToFulcrumResponse();
            app.UseMvc();
        }

        private static IGdprCapability CreateGdprCapability(bool useMock = false)
        {
            IStorage storage;
            if (useMock)
            {
                storage = new MemoryStorage();
            }
            else
            {
                storage = new SqlServerStorage("Data Source=localhost;Initial Catalog=LeverExampleGdpr;Persist Security Info=True;User ID=LeverExampleGdprUser;Password=Password123!;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=True");
            }

            return new Mapper(storage);
        }
    }
}
