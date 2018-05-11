using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Storage.Model;
using Xlent.Lever.Libraries2.Core.Translation;
using System.ComponentModel.DataAnnotations;

namespace Frobozz.Contracts.GdprCapability.Model
{
    /// <summary>
    /// Information about a physical person.
    /// </summary>
    public class Person : PersonCreate, IRecommendedStorableItem<string>
    {
        #region Interface implementations
        /// <inheritdoc />
        [TranslationConcept("person.id")]
        [Key]
        public string Id { get; set; }

        /// <inheritdoc />
        public string Etag { get; set; }

        /// <inheritdoc />
        public override void Validate(string errorLocation, string propertyPath = "")
        {
            base.Validate(errorLocation, propertyPath);
            FulcrumValidate.IsNotNullOrWhiteSpace(Id, nameof(Id), errorLocation);
            FulcrumValidate.IsNotNullOrWhiteSpace(Etag, nameof(Etag), errorLocation);
        }
        #endregion
    }
}