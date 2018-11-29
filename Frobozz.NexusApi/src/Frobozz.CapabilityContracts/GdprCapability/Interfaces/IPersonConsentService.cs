using System.Threading;
using System.Threading.Tasks;
using Frobozz.Contracts.GdprCapability.Model;
using Nexus.Link.Libraries.Crud.Interfaces;

namespace Frobozz.Contracts.GdprCapability.Interfaces
{
    /// <inheritdoc cref="ICrudSlaveToMasterBasic{PersonConsentCreate,PersonConsent,string}" />
    public interface IPersonConsentService :
        ICreateSlaveWithSpecifiedId<PersonConsentCreate, PersonConsent, string>, 
        IReadSlave<PersonConsent, string>, IReadChildrenWithPaging<PersonConsent, string>, 
        IUpdateSlave<PersonConsent, string>, 
        IDeleteSlave<string>, IDeleteChildren<string>
    {
    }
}