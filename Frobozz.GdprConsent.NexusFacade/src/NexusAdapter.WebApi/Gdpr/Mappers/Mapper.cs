using System;
using Frobozz.CapabilityContracts.Gdpr;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Contracts;
using Xlent.Lever.Libraries2.Core.Storage.Model;
using Xlent.Lever.Libraries2.MoveTo.Core.Mapping;

namespace Frobozz.GdprConsent.NexusAdapter.WebApi.Gdpr.Mappers
{
    /// <inheritdoc />
    public class Mapper : IGdprCapability
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Mapper(IStorage storage)
        {
            PersonService = new PersonMapper(storage);
            ConsentService = new CrudMapper<Consent, string, IStorage, ConsentTable, Guid>(storage, storage.Consent, new ConsentModelMapper());
            PersonConsentService =
                new ManyToOneMapper<PersonConsent, string, IStorage, PersonConsentTable, Guid>(storage,
                    storage.PersonConsent, new PersonConsentModelMapper());
        }

        /// <inheritdoc />
        public IPersonService PersonService { get; }

        /// <inheritdoc />
        public ICrud<Consent, string> ConsentService { get; }

        /// <inheritdoc />
        public IManyToOneRelation<PersonConsent, string> PersonConsentService { get; }
    }
}