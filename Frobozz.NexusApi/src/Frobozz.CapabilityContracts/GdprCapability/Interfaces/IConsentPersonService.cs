using System.Threading;
using System.Threading.Tasks;
using Frobozz.Contracts.GdprCapability.Model;
using Xlent.Lever.Libraries2.Crud.Interfaces;

namespace Frobozz.Contracts.GdprCapability.Interfaces
{
    /// <inheritdoc cref="ICrudSlaveToMaster{TModelCreate,TModel,TId}" />
    public interface IConsentPersonService :
        IReadChildrenWithPaging<PersonConsent, string>
    {
    }
}