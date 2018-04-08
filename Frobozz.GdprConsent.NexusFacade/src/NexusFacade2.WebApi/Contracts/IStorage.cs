using System;
using Xlent.Lever.Libraries2.Core.Storage.Model;

#pragma warning disable 1591

namespace Frobozz.GdprConsent.NexusFacade.WebApi.Contracts
{
    public interface IStorage
    {
        IManyToOneRelationComplete<AddressTable, Guid> Address { get; }
        ICrud<PersonTable, Guid> Person { get; }
        ICrud<ConsentTable, Guid> Consent { get; }
        IManyToOneRelationComplete<PersonConsentTable, Guid> PersonConsent { get; }
    }
}