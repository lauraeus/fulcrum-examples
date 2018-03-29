using System;
using Frobozz.GdprConsent.NexusFacade.WebApi.DalModel;
using Xlent.Lever.Libraries2.Core.Storage.Model;

namespace Frobozz.GdprConsent.NexusFacade.WebApi.Dal
{
    public interface IStorage
    {
        IManyToOneRelation<AddressTable, PersonTable, Guid> Address { get; }
        ICrud<PersonTable, Guid> Person { get; }
    }
}