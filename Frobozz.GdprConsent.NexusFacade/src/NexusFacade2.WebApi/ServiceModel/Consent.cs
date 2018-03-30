using System.Collections.Generic;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Storage.Model;

namespace Frobozz.GdprConsent.NexusFacade.WebApi.ServiceModel
{
    public class Consent : IValidatable
    {
        /// <summary>
        /// The name of the consent
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// True if the person has approved the consent
        /// </summary>
        public bool HasGivenConsent { get; set; }

        /// <inheritdoc />
        public void Validate(string errorLocation, string propertyPath = "")
        {
           FulcrumValidate.IsNotNullOrWhiteSpace(Name, nameof(Name), errorLocation);
        }
    }
}