using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Nexus.Link.Libraries.Core.Application;
using Nexus.Link.Libraries.Core.MultiTenant.Model;

namespace TheSystem.NexusAdapter.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Nexus.Link.Libraries.Web.AspNet.Application.FulcrumApplicationHelper.WebBasicSetup("TheSystem.NexusAdapter", new Tenant("local", "dev"), RunTimeLevelEnum.Development);

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
