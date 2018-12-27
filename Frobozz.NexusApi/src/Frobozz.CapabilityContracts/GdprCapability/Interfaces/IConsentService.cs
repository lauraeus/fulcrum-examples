using Frobozz.Contracts.GdprCapability.Model;
using Nexus.Link.Libraries.Crud.Interfaces;

namespace Frobozz.Contracts.GdprCapability.Interfaces
{
    /// <summary>
    /// Methods for accessing a person
    /// </summary>
    public interface IConsentService : ICrudBasic<ConsentCreate, Consent, string>
    {
    }
}