using System;
using Frobozz.GdprConsent.NexusFacade.WebApi.Dal.Model;
using Xlent.Lever.Libraries2.Core.Storage.Model;

namespace Frobozz.GdprConsent.NexusFacade.WebApi.Dal
{
    public class Storage : IStorage
    {
        public ICrud<PersonTable, Guid> Person { get; }
        public IManyToOneRelationComplete<AddressTable, PersonTable, Guid> Address { get; }

        public Storage(
            ICrud<PersonTable, Guid> personStorage,
            IManyToOneRelationComplete<AddressTable, PersonTable, Guid> addressStorage
            )
        {
            Person = personStorage;
            Address = addressStorage;
        }
    }
}