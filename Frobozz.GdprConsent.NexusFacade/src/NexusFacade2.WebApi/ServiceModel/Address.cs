using Xlent.Lever.Libraries2.Core.Assert;

namespace Frobozz.GdprConsent.NexusFacade.WebApi.ServiceModel
{
    enum AddressTypeEnum
    {
        Public,
        Invoice,
        Delivery,
        Postal
    }
    public class Address : IValidatable
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
    }
}