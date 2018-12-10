using System;
using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Dal.Contracts;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Mappers.Basic;
using Nexus.Link.Libraries.Crud.Interfaces;
using Nexus.Link.Libraries.Crud.Mappers;

namespace Frobozz.GdprConsent.NexusAdapter.WebApi.Mappers
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
            ConsentService = new ConsentMapper(storage, new Basic.ConsentMapper());
            PersonConsentService = new PersonConsentMapper(storage);
            ConsentPersonService = new ConsentPersonMapper(storage);
        }

        /// <inheritdoc />
        public IPersonService PersonService { get; }

        /// <inheritdoc />
        public IConsentService ConsentService { get; }

        /// <inheritdoc />
        public IPersonConsentService PersonConsentService { get; }

        /// <inheritdoc />
        public IConsentPersonService ConsentPersonService { get; }
    }
}