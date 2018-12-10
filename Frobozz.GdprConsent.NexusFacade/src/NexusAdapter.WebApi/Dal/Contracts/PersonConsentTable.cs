using System;
using Nexus.Link.Libraries.Core.Assert;
using Nexus.Link.Libraries.Core.Storage.Model;
using Nexus.Link.Libraries.SqlServer.Model;

namespace Frobozz.GdprConsent.NexusAdapter.WebApi.Dal.Contracts
{
    /// <summary>
    /// Many-to-many person-consent
    /// </summary>
    public class PersonConsentTable : ITableItem, ITimeStamped, IValidatable
    {
        /// <summary>
        /// True if the person has approved the consent
        /// </summary>
        public bool HasGivenConsent { get; set; }
        /// <summary>
        ///  The person that this address is for
        /// </summary>
        public Guid PersonId { get; set; }

        /// <summary>
        ///  The person that this address is for
        /// </summary>
        public Guid ConsentId { get; set; }

        /// <inheritdoc />
        public Guid Id { get; set; }

        /// <inheritdoc />
        public string Etag { get; set; }

        /// <inheritdoc />
        public DateTimeOffset RecordCreatedAt { get; set; }

        /// <inheritdoc />
        public DateTimeOffset RecordUpdatedAt { get; set; }

        /// <inheritdoc />
        public void Validate(string errorLocation, string propertyPath = "")
        {
            FulcrumValidate.IsNotDefaultValue(PersonId, nameof(PersonId), errorLocation);
            FulcrumValidate.IsNotDefaultValue(ConsentId, nameof(ConsentId), errorLocation);
        }
    }
}