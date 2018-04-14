using System;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Contracts;
using Xlent.Lever.Libraries2.Core.Crud.Interfaces;
using Xlent.Lever.Libraries2.Core.Crud.MemoryStorage;

namespace Frobozz.GdprConsent.NexusAdapter.WebApi.Dal.Mock
{
    /// <inheritdoc />
    public class MemoryStorage : IServerLogic
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