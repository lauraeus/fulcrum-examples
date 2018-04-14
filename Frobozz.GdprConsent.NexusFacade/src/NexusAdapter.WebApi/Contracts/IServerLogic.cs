using System;
using Xlent.Lever.Libraries2.Core.Crud.Interfaces;

#pragma warning disable 1591

namespace Frobozz.GdprConsent.NexusAdapter.WebApi.Contracts
{
    public interface IServerLogic
    {
        IManyToOneRelationComplete<AddressTable, Guid> Address { get; }
        ICrud<PersonTable, Guid> Person { get; }
        ICrud<ConsentTable, Guid> Consent { get; }
        IManyToOneRelationComplete<PersonConsentTable, Guid> PersonConsent { get; }
    }
}