using System.ComponentModel.DataAnnotations;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Storage.Model;
using Xlent.Lever.Libraries2.Core.Translation;

namespace Frobozz.Contracts.GdprCapability.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class PersonConsentCreate : IValidatable
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

        #region Interface implementations
        /// <inheritdoc />
        public virtual void Validate(string errorLocation, string propertyPath = "")
        {
            FulcrumValidate.IsNotNullOrWhiteSpace(PersonId, nameof(PersonId), errorLocation);
            FulcrumValidate.IsNotNullOrWhiteSpace(ConsentId, nameof(ConsentId), errorLocation);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"({PersonId}, {ConsentId})";
        }
        #endregion
    }
}