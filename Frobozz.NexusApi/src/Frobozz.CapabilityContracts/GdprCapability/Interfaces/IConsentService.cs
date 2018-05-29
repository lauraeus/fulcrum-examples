using System.Threading;
using System.Threading.Tasks;
using Frobozz.Contracts.GdprCapability.Model;
using Xlent.Lever.Libraries2.Crud.Interfaces;

namespace Frobozz.Contracts.GdprCapability.Interfaces
{
    /// <summary>
    /// Methods for accessing a person
    /// </summary>
    public interface IConsentService : ICrudBasic<ConsentCreate, Consent, string>
    {
    }
}