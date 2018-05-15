using System.ComponentModel.DataAnnotations;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Storage.Model;
using Xlent.Lever.Libraries2.Core.Translation;

namespace Frobozz.Contracts.GdprCapability.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class PersonConsent : PersonConsentCreate, IRecommendedStorableItem<string>, IValidatable
    {
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
        public override void Validate(string errorLocation, string propertyPath = "")
        {
            FulcrumValidate.IsNotNullOrWhiteSpace(ConsentName, nameof(ConsentName), errorLocation);
            FulcrumValidate.IsNotNullOrWhiteSpace(Id, nameof(Id), errorLocation);
            FulcrumValidate.IsNotNullOrWhiteSpace(Etag, nameof(Etag), errorLocation);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{ConsentName} ({PersonId})";
        }
        #endregion
    }
}