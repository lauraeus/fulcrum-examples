using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Translation;

namespace Frobozz.CapabilityContracts.Gdpr.Model
{
    public class Address : IValidatable
    {
        [TranslationConcept("person.address.type.code")]
        public string Type { get; set; }
        /// <summary>
        /// The street part of the address
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// The city part of the address
        /// </summary>
        public string City { get; set; }

        #region Interface implementations
        /// <inheritdoc />
        public void Validate(string errorLocation, string propertyPath = "")
        {
            FulcrumValidate.IsNotNullOrWhiteSpace(Type, nameof(Type), errorLocation);
            FulcrumValidate.IsNotNullOrWhiteSpace(City, nameof(City), errorLocation);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Street}, {City} ({Type})";
        }

        #endregion
    }
}