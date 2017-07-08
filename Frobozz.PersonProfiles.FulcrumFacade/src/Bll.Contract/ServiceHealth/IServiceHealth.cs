using System.Threading.Tasks;
using Xlent.Lever.Libraries2.Standard.Health.Model;

namespace Xlent.Lever.KeyTranslator.Facade.WebApi.Controllers
{
    public interface IServiceHealth
    {
        Task<HealthResponse> ServiceHealthAsync();
    }
}