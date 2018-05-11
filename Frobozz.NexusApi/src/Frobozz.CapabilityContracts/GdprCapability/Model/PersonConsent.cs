using System.ComponentModel.DataAnnotations;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Storage.Model;
using Xlent.Lever.Libraries2.Core.Translation;

namespace Frobozz.Contracts.GdprCapability.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class PersonConsent : IRecommendedStorableItem<string>, IValidatable
    {
        /// <summary>
        /// True if the person has approved the consent
        /// </summary>
        public bool HasGivenConsent { get; set; }

        /// <summary>
        /// The id of the person that this person-consent is for.
        /// </summary>
        [TranslationConcept("person.id")]
        [Required]
        public string PersonId { get; set; }

        /// <summary>
        /// The id of the consent that this person-consent is for.
        /// </summary>
        [TranslationConcept("consent.id")]
        [Required]
        public string ConsentId { get; set; }

        /// <summary>
        /// The name of the consent.
        /// </summary>
        public string ConsentName { get; set; }

        #region Interface implementations
        /// <inheritdoc />
        [TranslationConcept("person-consent.id")]
        [Key]
        public string Id { get; set; }
        /// <inheritdoc />
        public string Etag { get; set; }

        /// <inheritdoc />
        public void Validate(string errorLocation, string propertyPath = "")
        {
            FulcrumValidate.IsNotNullOrWhiteSpace(ConsentName, nameof(ConsentName), errorLocation);
            FulcrumValidate.IsNotNull(PersonId, nameof(PersonId), errorLocation);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{ConsentName} ({PersonId})";
        }
        #endregion
    }
}