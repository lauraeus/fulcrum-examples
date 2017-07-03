using System;
using Xlent.Lever.Libraries2.Standard.Assert;
using Xlent.Lever.Libraries2.Standard.Storage.Model;

namespace Frobozz.PersonProfiles.Dal.Contracts.PersonProfile
{
    /// <summary>
    /// A storable person profile class.
    /// </summary>
    public partial class StorablePersonProfile : IStorablePersonProfile
    {
        /// <summary>
        /// The given name (western "first name") for the person.
        /// </summary>
        public string GivenName { get; set; }

        /// <summary>
        /// The surname (western "last name") for the person.
        /// </summary>
        public string Surname { get; set; }
    }

    public partial class StorablePersonProfile : StorableItem<Guid>
    {
        #region INameProperty
        /// <inheritdoc />
        public override string Name => $"{GivenName} {Surname}";
        #endregion

        #region IValidatable
        /// <inheritdoc />
        public override void Validate(string errorLocation, string propertyPath = "")
        {
            FulcrumValidate.IsNotNullOrWhiteSpace(GivenName, nameof(GivenName), errorLocation);
            FulcrumValidate.IsNotNullOrWhiteSpace(Surname, nameof(Surname), errorLocation);
        }
        #endregion
    }
}
