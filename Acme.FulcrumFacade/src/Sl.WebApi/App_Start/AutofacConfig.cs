using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Acme.FulcrumFacade.Bll;
using Acme.FulcrumFacade.Bll.Contract.Bll.Interface;
using Acme.FulcrumFacade.Bll.Contract.Dal;
using Acme.FulcrumFacade.Dal.Memory;

namespace Acme.FulcrumFacade.Sl.WebApi
{
    public class AutofacConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            //Register Bll and Dal dependencies
            builder.RegisterType<ProductLogic>().As<IProductLogic>();
            builder.RegisterType<MockProductRepository>().As<IProductRepository>();

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

           //Register a IDependencyResolver used by WebApi 
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}