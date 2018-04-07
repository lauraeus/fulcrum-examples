using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Storage.Model;

namespace Frobozz.CapabilityContracts.Gdpr
{
    public class PersonConsent : StorableItem<string>
    {
        /// <summary>
        /// The name of the consent
        /// </summary>
        public string ConsentName { get; set; }

        /// <summary>
        /// The id of the person that this consent is for.
        /// </summary>
        public string PersonId { get; set; }

        /// <summary>
        /// True if the person has approved the consent
        /// </summary>
        public bool HasGivenConsent { get; set; }

        /// <summary>
        /// The id of the consent.
        /// </summary>
        public string ConsentId { get; set; }

        /// <inheritdoc />
        public override void Validate(string errorLocation, string propertyPath = "")
        {
            FulcrumValidate.IsNotNullOrWhiteSpace(ConsentName, nameof(ConsentName), errorLocation);
            FulcrumValidate.IsNotNull(PersonId, nameof(PersonId), errorLocation);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{ConsentName} ({PersonId})";
        }
    }
}