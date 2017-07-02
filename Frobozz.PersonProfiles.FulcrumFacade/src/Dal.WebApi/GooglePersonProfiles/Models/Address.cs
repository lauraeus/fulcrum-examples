using Xlent.Lever.Libraries2.Standard.Assert;

namespace Frobozz.PersonProfiles.Dal.WebApi.GooglePersonProfiles.Models
{
    public class Address : IValidatable
    {
        public string Row1;
        public string Row2;
        public string PostTown;
        public string PostCode;
        public string Country;
        public void Validate(string errorLocation, string propertyPath = "")
        {
            FulcrumValidate.IsNotNullOrWhiteSpace(Row1, nameof(Row1), errorLocation);
            FulcrumValidate.IsNotNullOrWhiteSpace(PostCode, nameof(PostCode), errorLocation);
            FulcrumValidate.IsNotNullOrWhiteSpace(Country, nameof(Country), errorLocation);
        }
    }
}
