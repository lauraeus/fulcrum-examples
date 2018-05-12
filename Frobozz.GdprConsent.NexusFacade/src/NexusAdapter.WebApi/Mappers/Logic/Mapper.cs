using System;
using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Dal.Contracts;
using Xlent.Lever.Libraries2.Core.Crud.Interfaces;
using Xlent.Lever.Libraries2.Core.Crud.Mappers;

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
            ConsentService = new CrudMapper<ConsentCreate,Consent,string,ConsentTable,Guid>(storage.Consent, new ConsentMapper());
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