using System;
using Nexus.Link.Libraries.Core.Assert;
using Nexus.Link.Libraries.Core.Storage.Model;
using Nexus.Link.Libraries.SqlServer.Model;

namespace Frobozz.GdprConsent.Logic.Dal.Contracts
{
    /// <summary>
    /// Information about a person
    /// </summary>
    public class PersonTable : ITableItem, ITimeStamped, IValidatable
    {
        /// <summary>
        /// The name of the person
        /// </summary>
        public string Name { get; set; }

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
            FulcrumValidate.IsNotNullOrWhiteSpace(Name, nameof(Name), errorLocation);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Name}";
        }
    }
}