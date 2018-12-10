using System;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Dal.Contracts;
using Nexus.Link.Libraries.Crud.Interfaces;
using Nexus.Link.Libraries.Crud.MemoryStorage;

namespace Frobozz.GdprConsent.NexusAdapter.WebApi.Dal.Mock
{
    /// <inheritdoc />
    public class MemoryStorage : IStorage
    {
        /// <inheritdoc />
        public ICrud<PersonTable, Guid> Person { get; }

        /// <inheritdoc />
        public ICrud<ConsentTable, Guid> Consent { get; }

        /// <inheritdoc />
        public ICrudManyToOne<AddressTable, Guid> Address { get; }

        /// <inheritdoc />
        public ICrudManyToMany<PersonConsentTable, PersonTable, ConsentTable, Guid> PersonConsent { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        public MemoryStorage()
        {
            Person = new CrudMemory<PersonTable, Guid>();
            Consent = new CrudMemory<ConsentTable, Guid>();
            Address = new ManyToOneMemory<AddressTable, Guid>(item => item.PersonId);
            PersonConsent =
                new ManyToManyMemory<PersonConsentTable, PersonTable, ConsentTable, Guid>(item => item.PersonId, item => item.ConsentId, Person, Consent);
        }
    }
}