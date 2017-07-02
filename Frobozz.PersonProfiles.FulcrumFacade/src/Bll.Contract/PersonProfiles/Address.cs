using Xlent.Lever.Libraries2.Standard.Assert;

namespace Frobozz.PersonProfiles.FulcrumFacade.Contract.PersonProfiles
{
    /// <summary>
    /// A physical address.
    /// </summary>
    public class Address : IValidatable
    {
        /// <summary>
        /// The first row of the address.
        /// </summary>
        public string Row1 { get; set; }
        /// <summary>
        /// An optional second reow of the address.
        /// </summary>
        public string Row2 { get; set; }
        /// <summary>
        /// The post code
        /// </summary>
        public string PostCode { get; set; }
        /// <summary>
        /// The post town
        /// </summary>
        public string PostTown { get; set; }
        /// <summary>
        /// The post town
        /// </summary>
        public string Country { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{Row1} {PostTown} {Country}";
        }

        /// <inheritdoc />
        public void Validate(string errorLocation, string propertyPath = "")
        {
            FulcrumValidate.IsNotNullOrWhiteSpace(Row1, nameof(Row1), errorLocation);
            FulcrumValidate.IsNotNullOrWhiteSpace(PostCode, nameof(PostCode), errorLocation);
            FulcrumValidate.IsNotNullOrWhiteSpace(Country, nameof(Country), errorLocation);
        }
    }
}
