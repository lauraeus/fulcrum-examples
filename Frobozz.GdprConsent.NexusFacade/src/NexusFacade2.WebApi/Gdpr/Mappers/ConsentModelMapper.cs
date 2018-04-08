using System;
using System.Threading;
using System.Threading.Tasks;
using Frobozz.CapabilityContracts.Gdpr;
using Frobozz.GdprConsent.NexusFacade.WebApi.Contracts;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.MoveTo.Core.Mapping;

namespace Frobozz.GdprConsent.NexusFacade.WebApi.Gdpr.Mappers
{
    /// <inheritdoc />
    public class ConsentModelMapper : IModelMapper<Consent, IStorage, ConsentTable>
    {
        /// <inheritdoc />
        public async Task<Consent> CreateAndMapFromServerAsync(ConsentTable source, IStorage logic,
            CancellationToken token = default(CancellationToken))
        {
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var target = new Consent
            {
                Id = MapperHelper.MapId<string, Guid>(source.Id),
                Name = source.Name,
                Etag = source.Etag
            };
            FulcrumAssert.IsValidated(target);
            return await Task.FromResult(target);
        }

        /// <inheritdoc />
        public async Task<ConsentTable> CreateAndMapToServerAsync(Consent source, IStorage logic, CancellationToken token = default(CancellationToken))
        {
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var id = MapperHelper.MapId<Guid, string>(source.Id);
            var target = new ConsentTable
            {
                Id = id,
                Name = source.Name,
                Etag = source.Etag
            };
            FulcrumAssert.IsValidated(target);
            return await Task.FromResult(target);
        }
    }
}