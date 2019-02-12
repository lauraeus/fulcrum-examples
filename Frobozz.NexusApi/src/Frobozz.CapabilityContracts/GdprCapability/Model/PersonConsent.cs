using System.ComponentModel.DataAnnotations;
using Nexus.Link.Libraries.Core.Assert;
using Nexus.Link.Libraries.Core.Storage.Model;
using Nexus.Link.Libraries.Core.Translation;

namespace Frobozz.Contracts.GdprCapability.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class PersonConsent : PersonConsentCreate, IRecommendedStorableItem<string>, IValidatable
    {
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
            FulcrumValidate.IsNotNullOrWhiteSpace(Id, nameof(Id), errorLocation);
            FulcrumValidate.IsNotNullOrWhiteSpace(Etag, nameof(Etag), errorLocation);
        }
        #endregion
    }
}