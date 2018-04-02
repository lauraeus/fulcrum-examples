using Frobozz.CapabilityContracts.Core.Translation;
using Xlent.Lever.Libraries2.Core.Assert;

namespace Frobozz.CapabilityContracts.Gdpr
{
    public enum AddressTypeEnum
    {

        None,
        Public,
        Invoice,
        Delivery,
        Postal
    }
    public class Address : IValidatable, ITranslatable
    {
        public string Type;
        /// <summary>
        /// The street part of the address
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// The city part of the address
        /// </summary>
        public string City { get; set; }

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

        /// <inheritdoc />
        public void DecorateForTranslation(string clientName)
        {
            Type = TranslationHelper.Decorate("person.address.type", clientName, Type);
        }
    }
}