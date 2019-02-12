using Nexus.Link.Libraries.Core.Assert;
using Nexus.Link.Libraries.Core.Storage.Model;
using Nexus.Link.Libraries.Core.Translation;
using System.ComponentModel.DataAnnotations;

namespace Frobozz.Contracts.GdprCapability.Model
{
    /// <summary>
    /// Information about a specific consent.
    /// </summary>
    public class Consent : ConsentCreate, IRecommendedStorableItem<string>
    {
        #region Interface implementations
        /// <inheritdoc />
        [TranslationConcept("consent.id")]
        [Key]
        public string Id { get; set; }

        /// <inheritdoc />
        public string Etag { get; set; }

        /// <inheritdoc />
        public override void Validate(string errorLocation, string propertyPath = "")
        {
            base.Validate(errorLocation, propertyPath);
            FulcrumValidate.IsNotNullOrWhiteSpace(Name, nameof(Name), errorLocation);
            FulcrumValidate.IsNotNullOrWhiteSpace(Id, nameof(Id), errorLocation);
            FulcrumValidate.IsNotNullOrWhiteSpace(Etag, nameof(Etag), errorLocation);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Name}";
        }
        #endregion
    }
}