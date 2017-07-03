using System;
using Frobozz.PersonProfiles.FulcrumFacade.Contract.PersonProfiles;
using Xlent.Lever.Libraries2.Standard.Assert;
using Xlent.Lever.Libraries2.Standard.Storage.Model;

namespace Frobozz.PersonProfiles.FulcrumFacade.WebApi.Models
{
    /// <summary>
    /// A physical address.
    /// </summary>
    public partial class ServicePersonProfile : StorableItem<string>, IPersonProfile
    {
        /// <inheritdoc />
        public string GivenName { get; set; }

        /// <inheritdoc />
        public string Surname { get; set; }

        #region IValidatable
        /// <inheritdoc />
        public override void Validate(string errorLocation, string propertyPath = "")
        {
            FulcrumValidate.IsNotNullOrWhiteSpace(GivenName, nameof(GivenName), errorLocation);
            FulcrumValidate.IsNotNullOrWhiteSpace(Surname, nameof(Surname), errorLocation);
        }
        #endregion
    }

    public partial class ServicePersonProfile
    {
        internal static ServicePersonProfile ToImplementation(IPersonProfile item)
        {
            var implementation = item as ServicePersonProfile;
            InternalContract.RequireNotNull(implementation, nameof(item), $"Contract violation. Expected parameter {nameof(item)} to be of type {nameof(ServicePersonProfile)}, but was of type {item.GetType().Name}");
            return implementation;
        }

        #region override object
        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{GivenName} {Surname}";
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            var person = obj as ServicePersonProfile;
            if (person == null) return false;
            if (!Equals(person.Id, Id)) return false;
            if (!string.Equals(person.ETag, ETag, StringComparison.OrdinalIgnoreCase)) return false;
            if (!string.Equals(person.GivenName, GivenName, StringComparison.OrdinalIgnoreCase)) return false;
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (!string.Equals(person.Surname, Surname, StringComparison.OrdinalIgnoreCase)) return false;
            return true;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return Id.GetHashCode();
        }
        #endregion
    }
}
