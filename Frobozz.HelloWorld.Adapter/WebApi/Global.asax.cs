using System.Web.Http;
using Xlent.Lever.Libraries2.Core.Application;
using FulcrumApplicationHelper = Xlent.Lever.Libraries2.WebApi.Application.FulcrumApplicationHelper;

namespace Frobozz.HelloWorld.Adapter
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	public class WebApiApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
            FulcrumApplicationHelper.WebApiBasicSetup(new ConfigurationManagerAppSettings());
            GlobalConfiguration.Configure( WebApiConfig.Register );
			GlobalConfiguration.Configure( SwaggerConfig.Register );
			GlobalConfiguration.Configure( AutofacConfig.Register );
            GlobalConfiguration.Configuration.EnsureInitialized();

		}
	}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
