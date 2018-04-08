using System;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Storage.Model;
using Xlent.Lever.Libraries2.SqlServer.Model;

namespace Frobozz.GdprConsent.NexusAdapter.WebApi.Contracts
{
    /// <summary>
    /// Information about a consent
    /// </summary>
    public class ConsentTable : StorableItem, ITimeStamped, ITableItem
    {
        /// <summary>
        /// The name of the person
        /// </summary>
        public string Name { get; set; }

        /// <inheritdoc />
        public DateTimeOffset RecordCreatedAt { get; set; }

        /// <inheritdoc />
        public DateTimeOffset RecordUpdatedAt { get; set; }

        /// <inheritdoc />
        public override void Validate(string errorLocation, string propertyPath = "")
        {
           FulcrumValidate.IsNotNullOrWhiteSpace(Name, nameof(Name), errorLocation);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Name}";
        }
    }
}