using System;
using Frobozz.GdprConsent.NexusFacade.WebApi.Dal.Model;
using Xlent.Lever.Libraries2.Core.Storage.Model;

namespace Frobozz.GdprConsent.NexusFacade.WebApi.Dal
{
    public interface IStorage
    {
        IManyToOneRelationComplete<AddressTable, PersonTable, Guid> Address { get; }
        ICrud<PersonTable, Guid> Person { get; }
    }
}