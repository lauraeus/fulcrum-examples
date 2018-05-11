using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Translation;
using System.ComponentModel.DataAnnotations;

namespace Frobozz.Contracts.GdprCapability.Model
{
    /// <summary>
    /// The address of a person
    /// </summary>
    public class Address : IValidatable
    {
        /// <summary>
        /// The type of address, i.e. is it a postal adressess, a visit address, etc.
        /// </summary>
        [TranslationConcept("person.address.type.code")]
        [Required]
        public string Type { get; set; }

        /// <summary>
        /// The street part of the address
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// The city part of the address
        /// </summary>
        [Required]
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