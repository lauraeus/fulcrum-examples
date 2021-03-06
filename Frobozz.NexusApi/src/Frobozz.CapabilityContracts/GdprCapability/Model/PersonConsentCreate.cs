﻿using System.ComponentModel.DataAnnotations;
using Nexus.Link.Libraries.Core.Assert;
using Nexus.Link.Libraries.Core.Translation;

namespace Frobozz.Contracts.GdprCapability.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class PersonConsentCreate : IValidatable
    {

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
        /// True if the person has approved the consent
        /// </summary>
        public bool HasGivenConsent { get; set; }

        /// <summary>
        /// The name of the person.
        /// </summary>
        public string PersonName { get; set; }

        /// <summary>
        /// The name of the consent.
        /// </summary>
        public string ConsentName { get; set; }

        #region Interface implementations
        /// <inheritdoc />
        public virtual void Validate(string errorLocation, string propertyPath = "")
        {
            FulcrumValidate.IsNotNullOrWhiteSpace(PersonId, nameof(PersonId), errorLocation);
            FulcrumValidate.IsNotNullOrWhiteSpace(ConsentId, nameof(ConsentId), errorLocation);
            FulcrumValidate.IsNotNullOrWhiteSpace(ConsentName, nameof(ConsentName), errorLocation);
            FulcrumValidate.IsNotNullOrWhiteSpace(PersonName, nameof(PersonName), errorLocation);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"({PersonName}, {ConsentName} : {HasGivenConsent})";
        }
        #endregion
    }
}