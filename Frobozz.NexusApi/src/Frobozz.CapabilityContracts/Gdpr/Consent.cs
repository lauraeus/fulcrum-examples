using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Storage.Model;

namespace Frobozz.CapabilityContracts.Gdpr
{
    public class Consent : IValidatable, IUniquelyIdentifiable<string>
    {
        /// <inheritdoc />
        public string Id { get; set; }

        /// <summary>
        /// The id of the person that this consent is for.
        /// </summary>
        public string PersonId { get; set; }
        /// <summary>
        /// The name of the consent
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// True if the person has approved the consent
        /// </summary>
        public bool HasGivenConsent { get; set; }

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