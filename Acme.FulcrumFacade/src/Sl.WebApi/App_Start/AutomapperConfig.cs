using Acme.FulcrumFacade.Sl.WebApi.Model;

namespace Acme.FulcrumFacade.Sl.WebApi
{
    public class AutomapperConfig
    {
        public static void RegisterMappings()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Product, Bll.Contract.Bll.Model.Product>().ReverseMap();
            });

            AutoMapper.Mapper.AssertConfigurationIsValid();
        }
    }
}