using System;
using Frobozz.CapabilityContracts.Gdpr.Logic;
using Frobozz.CapabilityContracts.Gdpr.Model;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Contracts;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Mappers.Model;
using Xlent.Lever.Libraries2.Core.Storage.Model;
using Xlent.Lever.Libraries2.MoveTo.Core.Crud.Mapping;

namespace Frobozz.GdprConsent.NexusAdapter.WebApi.Mappers.Logic
{
    /// <inheritdoc />
    public class Mapper : IGdprCapability
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Mapper(IServerLogic serverLogic)
        {
            PersonService = new PersonMapper(serverLogic);
            ConsentService = new CrudMapper<Consent, string, IServerLogic, ConsentTable, Guid>(serverLogic, serverLogic.Consent, new ConsentModelMapper());
            PersonConsentService =
                new ManyToOneMapper<PersonConsent, string, IServerLogic, PersonConsentTable, Guid>(serverLogic,
                    serverLogic.PersonConsent, new PersonConsentModelMapper());
        }

        /// <inheritdoc />
        public IPersonService PersonService { get; }

        /// <inheritdoc />
        public ICrud<Consent, string> ConsentService { get; }

        /// <inheritdoc />
        public IManyToOneRelation<PersonConsent, string> PersonConsentService { get; }
    }
}