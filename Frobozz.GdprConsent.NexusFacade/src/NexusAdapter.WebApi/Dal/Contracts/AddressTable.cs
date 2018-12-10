using System;
using Nexus.Link.Libraries.Core.Assert;
using Nexus.Link.Libraries.Core.Storage.Model;
using Nexus.Link.Libraries.SqlServer.Model;

namespace Frobozz.GdprConsent.NexusAdapter.WebApi.Dal.Contracts
{
    /// <summary>
    /// Addres information for a person
    /// </summary>
    public class AddressTable : ITableItem, ITimeStamped, IValidatable
    {
        /// <summary>
        /// The type of address
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// The street part of the address
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// The city part of the address
        /// </summary>
        public string City { get; set; }

        /// <summary>
        ///  The person that this address is for
        /// </summary>
        public Guid PersonId { get; set; }

        /// <inheritdoc />
        public DateTimeOffset RecordCreatedAt { get; set; }

        /// <inheritdoc />
        public DateTimeOffset RecordUpdatedAt { get; set; }

        /// <inheritdoc />
        public Guid Id { get; set; }

        /// <inheritdoc />
        public string Etag { get; set; }

        /// <inheritdoc />
        public void Validate(string errorLocation, string propertyPath = "")
        {
            FulcrumValidate.IsNotNullOrWhiteSpace(City, nameof(City), errorLocation);
            FulcrumValidate.IsNotDefaultValue(PersonId, nameof(PersonId), errorLocation);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Street}, {City} ({Type})";
        }
    }
}