using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.MoveTo.Core.Translation;

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
        public void DecorateForTranslation(Translator translator)
        {
            Type = translator.Decorate("person.address.type.code", Type);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Street}, {City} ({Type})";
        }
    }
}