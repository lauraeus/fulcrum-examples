using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Storage.Model;
using Xlent.Lever.Libraries2.MoveTo.Core.Translation;

namespace Frobozz.CapabilityContracts.Gdpr
{
    public class PersonConsent : StorableItem<string>, ITranslatable
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

        /// <inheritdoc />
        public void DecorateForTranslation(Translator translator)
        {
            Id = translator.Decorate("person-consent.id", Id);
            PersonId = translator.Decorate("person.id", PersonId);
            ConsentId = translator.Decorate("consent.id", ConsentId);
        }
    }
}