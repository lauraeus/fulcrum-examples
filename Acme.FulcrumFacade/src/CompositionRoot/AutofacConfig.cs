using System.Reflection;
using System.Web.Http;
using Acme.FulcrumFacade.Bll.Contract.Product;
using Acme.FulcrumFacade.Bll.Product;
using Acme.FulcrumFacade.Dal.Contract.Product;
using Acme.FulcrumFacade.Dal.Memory;
using Autofac;
using Autofac.Integration.WebApi;

namespace Acme.FulcrumFacade.CompositionRoot
{
    public class AutofacConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            //Register Bll and Dal dependencies
            builder.RegisterType<ProductLogic>().As<Bll.Contract.Product.IProductLogic>();
            builder.RegisterType<ProductStorage>().As<IProductStorage>();

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

           //Register a IDependencyResolver used by WebApi 
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}