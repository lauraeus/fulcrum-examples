using System.Collections.Generic;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Storage.Model;
using Xlent.Lever.Libraries2.Core.Translation;

namespace Frobozz.CapabilityContracts.Gdpr.Model
{
    public class Person : IRecommendedStorableItem<string>, IValidatable
    {
        /// <summary>
        /// The name of the person
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The addresses for this person.
        /// </summary>
        public IEnumerable<Address> Addresses { get; set; }

        #region Interface implementations
        /// <inheritdoc />
        [TranslationConcept("person.id")]
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