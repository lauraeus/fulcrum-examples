using System.Threading.Tasks;
using Xlent.Lever.Libraries2.Core.Storage.Model;

namespace Frobozz.CapabilityContracts.Gdpr
{
    public interface IPersonService : ICrud<Person, string>
    {
        Task<Person> GetRandomAsync();
    }
}