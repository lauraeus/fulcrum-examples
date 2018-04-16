using System;
using Frobozz.CapabilityContracts.Gdpr.Logic;
using Frobozz.CapabilityContracts.Gdpr.Model;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Dal.Contracts;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Mappers.Model;
using Xlent.Lever.Libraries2.Core.Crud.Interfaces;
using Xlent.Lever.Libraries2.Core.Crud.Mapping;

namespace Frobozz.GdprConsent.NexusAdapter.WebApi.Mappers.Logic
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