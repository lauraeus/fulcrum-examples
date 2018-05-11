using System;
using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Dal.Contracts;
using Xlent.Lever.Libraries2.Core.Crud.Interfaces;

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
            ConsentService = new ConsentMapper(storage);
            PersonConsentService = new PersonConsentMapper(storage);
        }

        /// <inheritdoc />
        public IPersonService PersonService { get; }

        /// <inheritdoc />
        public ICrud<ConsentCreate, Consent, string> ConsentService { get; }

        /// <inheritdoc />
        public IManyToOne<PersonConsent, string> PersonConsentService { get; }
    }
}