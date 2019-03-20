using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nexus.Link.Libraries.Core.Application;
using Nexus.Link.Libraries.Core.MultiTenant.Model;
using Nexus.Link.Libraries.Web.AspNet.Application;

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
