using System;
using Frobozz.GdprConsent.NexusFacade.WebApi.Dal.Model;
using Xlent.Lever.Libraries2.Core.Storage.Model;

namespace Frobozz.GdprConsent.NexusFacade.WebApi.Dal
{
    /// <inheritdoc />
    public class Storage : IStorage
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
        public Storage(
            ICrud<PersonTable, Guid> personStorage,
            IManyToOneRelationComplete<AddressTable, Guid> addressStorage,
            ICrud<ConsentTable, Guid> consent,
            IManyToOneRelationComplete<PersonConsentTable, Guid> personConsent)
        {
            Person = personStorage;
            Address = addressStorage;
            Consent = consent;
            PersonConsent = personConsent;
        }
    }
}