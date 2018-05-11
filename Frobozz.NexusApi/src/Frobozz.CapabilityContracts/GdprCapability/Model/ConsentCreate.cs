using Xlent.Lever.Libraries2.Core.Assert;
using System.ComponentModel.DataAnnotations;

namespace Frobozz.Contracts.GdprCapability.Model
{
    /// <summary>
    /// Information about a specific consent.
    /// </summary>
    public class ConsentCreate : IValidatable
    {
        /// <summary>
        /// The name of the consent
        /// </summary>
        [Required]
        public string Name { get; set; }

        #region Interface implementations
        /// <inheritdoc />
        public virtual void Validate(string errorLocation, string propertyPath = "")
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