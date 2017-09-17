using System.Threading.Tasks;
using System.Web.Http;
using Xlent.Lever.KeyTranslator.LogicProvider;
using Xlent.Lever.Libraries2.Standard.Assert;
using Xlent.Lever.Libraries2.Standard.Health.Logic;
using Xlent.Lever.Libraries2.Standard.Health.Model;
using Xlent.Lever.Libraries2.Standard.MultiTenant.Model;

namespace Xlent.Lever.KeyTranslator.Facade.WebApi.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/v1/ServiceMetas")]
    public class ServiceMetasController : ApiController, IServiceHealth
    {
        private static readonly string Namespace = typeof(ServiceMetasController).Namespace;
        private readonly IExtendedLogicProvider _keyTranslatorLogic;

        public ServiceMetasController(IExtendedLogicProvider keyTranslatorLogic)
        {
            _keyTranslatorLogic = keyTranslatorLogic;
        }

        [Route("ServiceHealth")]
        [HttpGet]
        public async Task<HealthResponse> ServiceHealthAsync()
        {
            FulcrumAssert.IsValidatedAndNotNull(tenant, $"{Namespace}: 4B819109-2E18-481F-B001-D87F026D688C");
            var aggregator = new ResourceHealthAggregator(tenant, "Lever KeyTranslator Facade");
            // TODO: (XF-39) Implement service health in ICoreLogicProvider
            //aggregator.AddResourceHealth("Database", _keyTranslatorLogic);
            var result = aggregator.GetAggregatedHealthResponse();
            var response = new HealthResponse
            {
                Resource = "Frobozz.PersonProfiles",
                Status = HealthResponse.StatusEnum.Ok
            };
            return await Task.FromResult(result);
        }
    }
}
