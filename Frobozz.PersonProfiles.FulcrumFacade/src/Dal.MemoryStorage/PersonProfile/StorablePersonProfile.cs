using System;
using Newtonsoft.Json;
using Xlent.Lever.Libraries2.Standard.Assert;
using Xlent.Lever.Libraries2.Standard.Misc.Models;
using Xlent.Lever.Libraries2.Standard.Storage.Model;
using Xlent.Lever.Libraries2.Standard.Storage.Test;

namespace Frobozz.PersonProfiles.Dal.MemoryStorage.PersonProfile
{
    /// <summary>
    /// A storable person profile class.
    /// </summary>
    public partial class StorablePersonProfile : StorableItem<Guid>
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
    public partial class StorablePersonProfile : IDeepCopy<StorablePersonProfile>, IStorableItemForTesting<StorablePersonProfile, Guid>
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

        #region IDeepCopy
        /// <inheritdoc />
        public StorablePersonProfile DeepCopy()
        {
            var serialized = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<StorablePersonProfile>(serialized);
        }
        #endregion

        #region IStorableItemForTesting
        /// <inheritdoc />
        public StorablePersonProfile InitializeWithDataForTesting(TypeOfTestDataEnum typeOfTestData)
        {
            switch (typeOfTestData)
            {
                case TypeOfTestDataEnum.Variant1:
                    GivenName = "Joe";
                    Surname = "Smith";
                    break;
                case TypeOfTestDataEnum.Variant2:
                    GivenName = "Mary";
                    Surname = "Jones";
                    break;
                case TypeOfTestDataEnum.Random:
                    GivenName = Guid.NewGuid().ToString();
                    Surname = Guid.NewGuid().ToString();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(typeOfTestData), typeOfTestData, null);
            }
            return this;
        }

        /// <inheritdoc />
        public StorablePersonProfile ChangeDataToNotEqualForTesting()
        {
            GivenName = Guid.NewGuid().ToString();
            return this;
        }
        #endregion

        #region override object
        /// <inheritdoc/>
        public override string ToString()
        {
            return Name;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            var person = obj as StorablePersonProfile;
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
