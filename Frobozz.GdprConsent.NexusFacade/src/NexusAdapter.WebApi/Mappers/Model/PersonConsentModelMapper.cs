using System;
using System.Threading;
using System.Threading.Tasks;
using Frobozz.CapabilityContracts.Gdpr.Model;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Dal.Contracts;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Crud.Mappers;

namespace Frobozz.GdprConsent.NexusAdapter.WebApi.Mappers.Model
{
    /// <inheritdoc />
    public class PersonConsentModelMapper : ICrudModelMapper<PersonConsent, string, PersonConsentTable>
    {
        private readonly IStorage _storage;
        /// <summary>
        /// Constructor
        /// </summary>
        public  PersonConsentModelMapper(IStorage storage)
        {
            _storage = storage;
        }

        /// <inheritdoc />
        public async Task<PersonConsent> MapFromServerAsync(PersonConsentTable source,
            CancellationToken token = default(CancellationToken))
        {
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var serverConsent = await _storage.Consent.ReadAsync(source.ConsentId, token);
            var target = new PersonConsent
            {
                Id = MapperHelper.MapToType<string, Guid>(source.Id),
                ConsentId = MapperHelper.MapToType<string, Guid>(source.ConsentId),
                ConsentName = serverConsent.Name,
                Etag = source.Etag,
                PersonId = MapperHelper.MapToType<string, Guid>(source.PersonId),
                HasGivenConsent = source.HasGivenConsent
            };
            FulcrumAssert.IsValidated(target);
            return target;
        }

        /// <inheritdoc />
        public async Task<PersonConsentTable> MapToServerAsync(PersonConsent source, CancellationToken token = default(CancellationToken))
        {
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var target = new PersonConsentTable
            {
                Id = MapperHelper.MapToType<Guid, string>(source.Id),
                Etag = source.Etag,
                HasGivenConsent = source.HasGivenConsent,
                PersonId = MapperHelper.MapToType<Guid, string>(source.PersonId),
                ConsentId = MapperHelper.MapToType<Guid, string>(source.ConsentId),
            };
            FulcrumAssert.IsValidated(target);
            return await Task.FromResult(target);
        }

        /// <inheritdoc />
        public async Task<PersonConsentTable> CreateAndReturnAsync(PersonConsent source, CancellationToken token = new CancellationToken())
        {
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var target = await MapToServerAsync(source, token);
            target = await _storage.PersonConsent.CreateAndReturnAsync(target, token);
            FulcrumAssert.IsValidated(target);
            return await Task.FromResult(target);
        }

        /// <inheritdoc />
        public async Task<PersonConsentTable> CreateWithSpecifiedIdAndReturnAsync(string id, PersonConsent source,
            CancellationToken token = new CancellationToken())
        {
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var target = await MapToServerAsync(source, token);
            var serverId = MapperHelper.MapToType<Guid, string>(id);
            target = await _storage.PersonConsent.CreateWithSpecifiedIdAndReturnAsync(serverId, target, token);
            FulcrumAssert.IsValidated(target);
            return await Task.FromResult(target);
        }
    }
}