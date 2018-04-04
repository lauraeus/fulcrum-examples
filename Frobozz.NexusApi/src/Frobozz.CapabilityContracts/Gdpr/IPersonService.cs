using System.Threading.Tasks;
using Xlent.Lever.Libraries2.Core.Storage.Model;

namespace Frobozz.CapabilityContracts.Gdpr
{
    public interface IPersonService<TModel> : ICrud<TModel, string>
    where TModel : Person
    {
        Task<TModel> GetRandomAsync();
    }
}