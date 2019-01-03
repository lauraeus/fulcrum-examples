using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Nexus.Link.Libraries.Core.Application;
using Nexus.Link.Libraries.Core.MultiTenant.Model;

namespace Frobozz.GdprConsent.NexusAdapter.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            FulcrumApplicationHelper.RuntimeSetup("GdprConsent", new Tenant("local", "dev"), RunTimeLevelEnum.Development);
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                    logging.AddDebug();
                });
    }
}
