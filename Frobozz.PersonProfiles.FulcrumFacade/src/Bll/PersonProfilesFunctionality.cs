using System;
using System.Threading.Tasks;
using Frobozz.PersonProfiles.Dal.Contracts.PersonProfile;
using Frobozz.PersonProfiles.FulcrumFacade.Contract.PersonProfiles;
using Xlent.Lever.Libraries2.Standard.Assert;

namespace Frobozz.PersonProfiles.Bll
{
    public class PersonProfilesFunctionality : IPersonProfilesFunctionality
    {
        private readonly IPersonProfilePersistance<IStorablePersonProfile> _storage;

        public PersonProfilesFunctionality(IPersonProfilePersistance<IStorablePersonProfile> storage)
        {
            _storage = storage;
        }

        public async Task<IPersonProfile> CreateAsync(IPersonProfile item)
        {
            var dalPerson = await _storage.CreateAsync(ToDal(item));
            return ToService(dalPerson);
        }

        public async Task<IPersonProfile> ReadAsync(string id)
        {
            var dalPerson = await _storage.ReadAsync(ToGuid(id));
            return ToService(dalPerson);
        }

        public async Task<IPersonProfile> UpdateAsync(IPersonProfile item)
        {
            var dalPerson = await _storage.UpdateAsync(ToDal(item));
            return ToService(dalPerson);
        }

        public async Task DeleteAsync(string id)
        {
            await _storage.DeleteAsync(ToGuid(id));
        }

        private static IPersonProfile ToService(IStorablePersonProfile source)
        {
            if (source == null) return null;
            var target = new PersonProfile
            {
                Id = source.Id.ToString(),
                ETag = source.ETag,
                GivenName = source.GivenName,
                Surname = source.Surname
            };
            return target;
        }

        private static IStorablePersonProfile ToDal(IPersonProfile source)
        {
            if (source == null) return null;
            var target = new StorablePersonProfile
            {
                Id = ToGuid(source.Id),
                ETag = source.ETag,
                GivenName = source.GivenName,
                Surname = source.Surname
            };
            return target;
        }

        private static Guid ToGuid(string id)
        {
#pragma warning disable IDE0018 // Inline variable declaration
            Guid guid;
#pragma warning restore IDE0018 // Inline variable declaration
            InternalContract.Require(Guid.TryParse(id, out guid), $"Expected a Guid in {nameof(id)} but the value was ({id}).");
            return guid;
        }
    }
}