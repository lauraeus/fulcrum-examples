using Xlent.Lever.Libraries2.Standard.Storage.Model;

namespace Frobozz.PersonProfiles.FulcrumFacade.Contract.PersonProfiles
{
    /// <summary>
    /// The definition of a Person
    /// </summary>
    public interface IPersonProfile : IStorableItem<string>, IOptimisticConcurrencyControlByETag
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