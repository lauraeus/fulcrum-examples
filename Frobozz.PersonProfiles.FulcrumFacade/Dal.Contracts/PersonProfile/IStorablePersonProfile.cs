using System;
using Xlent.Lever.Libraries2.Standard.Storage.Model;

namespace Frobozz.PersonProfiles.Dal.Contracts.PersonProfile
{
    /// <summary>
    /// A storable person profile class.
    /// </summary>
    public interface IStorablePersonProfile : IRecommendedStorableItem<Guid>
    {
        /// <summary>
        /// The given name (western "first name") for the person.
        /// </summary>
        string GivenName { get; set; }

        /// <summary>
        /// The surname (western "last name") for the person.
        /// </summary>
        string Surname { get; set; }
    }
}
