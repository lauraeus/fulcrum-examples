using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Storage.Model;
using Xlent.Lever.Libraries2.MoveTo.Core.Translation;

namespace Frobozz.CapabilityContracts.Gdpr
{
    public class Consent : StorableItem<string>, ITranslatable
    {
        /// <summary>
        /// The name of the consent
        /// </summary>
        public string Name { get; set; }

        /// <inheritdoc />
        public override void Validate(string errorLocation, string propertyPath = "")
        {
           FulcrumValidate.IsNotNullOrWhiteSpace(Name, nameof(Name), errorLocation);
        }

        /// <inheritdoc />
        public void DecorateForTranslation(Translator translator)
        {
            Id = translator.Decorate("consent.id", Id);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Name}";
        }
    }
}