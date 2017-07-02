using System.Reflection;
using System.Web.Http;
using Xlent.Lever.CapabilityTemplate.Bll.Contract.Inbound.Product;
using Xlent.Lever.CapabilityTemplate.Bll.Product;
using Xlent.Lever.CapabilityTemplate.Dal.Contract.Product;
using Xlent.Lever.CapabilityTemplate.Dal.Memory;
using Autofac;
using Autofac.Integration.WebApi;

namespace Xlent.Lever.CapabilityTemplate.CompositionRoot
{
    public class AutofacConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            //Register Bll and Dal dependencies
            builder.RegisterType<Bll.Product.ProductFunctionality>().As<IProductFunctionality>();
            builder.RegisterType<ProductPersistance>().As<IProductPersistance>();

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

           //Register a IDependencyResolver used by WebApi 
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}