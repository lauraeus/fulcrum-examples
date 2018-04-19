using System;
using System.Threading;
using System.Threading.Tasks;
using Frobozz.CapabilityContracts.Gdpr.Model;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Dal.Contracts;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Crud.Interfaces;
using Xlent.Lever.Libraries2.Core.Crud.Mappers;

namespace Frobozz.GdprConsent.NexusAdapter.WebApi.Mappers.Model
{
    /// <inheritdoc />
    public class ConsentModelMapper : ICrudModelMapper<Consent, string, ConsentTable>
    {
        private IStorage _storage;

        /// <summary>
        /// Constructor
        /// </summary>
        public ConsentModelMapper(IStorage storage)
        {
            _storage = storage;
        }

        /// <inheritdoc />
        public async Task<Consent> MapFromServerAsync(ConsentTable source,
            CancellationToken token = default(CancellationToken))
        {
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var target = MapFromServer(source);
            return await Task.FromResult(target);
        }

        private Consent MapFromServer(ConsentTable source)
        {
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var target = new Consent
            {
                Id = MapperHelper.MapToType<string, Guid>(source.Id),
                Name = source.Name,
                Etag = source.Etag
            };
            FulcrumAssert.IsValidated(target);
            return target;
        }

        /// <inheritdoc />
        public async Task<ConsentTable> MapToServerAsync(Consent source, CancellationToken token = default(CancellationToken))
        {
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var id = MapperHelper.MapToType<Guid, string>(source.Id);
            var target = new ConsentTable
            {
                Id = id,
                Name = source.Name,
                Etag = source.Etag
            };
            FulcrumAssert.IsValidated(target);
            return await Task.FromResult(target);
        }

        /// <inheritdoc />
        public async Task<ConsentTable> CreateAndReturnAsync(Consent source, CancellationToken token = new CancellationToken())
        {
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var target = await MapToServerAsync(source, token);
            target = await _storage.Consent.CreateAndReturnAsync(target, token);
            FulcrumAssert.IsValidated(target);
            return await Task.FromResult(target);
        }

        /// <inheritdoc />
        public async Task<ConsentTable> CreateWithSpecifiedIdAndReturnAsync(string id, Consent source, CancellationToken token = new CancellationToken())
        {
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var target = await MapToServerAsync(source, token);
            var serverId = MapperHelper.MapToType<Guid, string>(id);
            target = await _storage.Consent.CreateWithSpecifiedIdAndReturnAsync(serverId, target, token);
            FulcrumAssert.IsValidated(target);
            return await Task.FromResult(target);
        }
    }
}