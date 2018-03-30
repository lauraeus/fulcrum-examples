using System;
using Frobozz.GdprConsent.NexusFacade.WebApi.Dal.Model;
using Xlent.Lever.Libraries2.Core.Storage.Model;

namespace Frobozz.GdprConsent.NexusFacade.WebApi.Dal
{
    public interface IStorage
    {
        IManyToOneRelationComplete<AddressTable, Guid> Address { get; }
        ICrud<PersonTable, Guid> Person { get; }
        ICrud<ConsentTable, Guid> Consent { get; }
        IManyToManyRelationComplete<PersonConsentTable, PersonTable, ConsentTable, Guid> PersonConsent { get; }
    }
}