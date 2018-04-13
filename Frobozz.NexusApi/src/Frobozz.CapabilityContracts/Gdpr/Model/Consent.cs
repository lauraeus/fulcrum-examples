using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Storage.Model;
using Xlent.Lever.Libraries2.MoveTo.Core.Translation;

namespace Frobozz.CapabilityContracts.Gdpr.Model
{
    public class Consent : IRecommendedStorableItem<string>, IValidatable
    {
        /// <summary>
        /// The name of the consent
        /// </summary>
        public string Name { get; set; }

        #region Interface implementations
        /// <inheritdoc />
        [TranslationConcept("consent.id")]
        public string Id { get; set; }

        /// <inheritdoc />
        public string Etag { get; set; }

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
        #endregion
    }
}