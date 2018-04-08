using System;
using Frobozz.GdprConsent.NexusFacade.WebApi.Contracts;
using Xlent.Lever.Libraries2.Core.Storage.Logic;
using Xlent.Lever.Libraries2.Core.Storage.Model;

namespace Frobozz.GdprConsent.NexusFacade.WebApi.Dal.Mock
{
    /// <inheritdoc />
    public class MemoryStorage : IStorage
    {
        /// <inheritdoc />
        public ICrud<PersonTable, Guid> Person { get; }

        /// <inheritdoc />
        public ICrud<ConsentTable, Guid> Consent { get; }

        /// <inheritdoc />
        public IManyToOneRelationComplete<AddressTable, Guid> Address { get; }

        /// <inheritdoc />
        public IManyToOneRelationComplete<PersonConsentTable, Guid> PersonConsent { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        public MemoryStorage()
        {
            Person = new CrudMemory<PersonTable, Guid>();
            Consent = new CrudMemory<ConsentTable, Guid>();
            Address = new ManyToOneMemory<AddressTable, Guid>(item => item.PersonId);
            PersonConsent =
                new ManyToOneMemory<PersonConsentTable, Guid>(item => item.PersonId);
        }
    }
}