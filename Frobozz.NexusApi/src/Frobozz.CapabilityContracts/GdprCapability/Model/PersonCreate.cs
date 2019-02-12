using System.Collections.Generic;
using Nexus.Link.Libraries.Core.Assert;
using System.ComponentModel.DataAnnotations;

namespace Frobozz.Contracts.GdprCapability.Model
{
    /// <summary>
    /// Information about a physical person.
    /// </summary>
    public class PersonCreate : IValidatable
    {
        /// <summary>
        /// The name of the person
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// The addresses for this person.
        /// </summary>
        public IEnumerable<Address> Addresses { get; set; }

        #region Interface implementations
        /// <inheritdoc />
        public virtual void Validate(string errorLocation, string propertyPath = "")
        {
            FulcrumValidate.IsNotNullOrWhiteSpace(Name, nameof(Name), errorLocation);
            FulcrumValidate.IsNotNull(Addresses, nameof(Addresses), errorLocation);
            FulcrumValidate.IsValidated(Addresses, propertyPath, nameof(Addresses), errorLocation);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Name}";
        }
        #endregion
    }
}