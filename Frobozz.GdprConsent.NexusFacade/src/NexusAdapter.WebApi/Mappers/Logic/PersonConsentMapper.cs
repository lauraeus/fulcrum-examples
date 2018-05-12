using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Frobozz.Contracts.GdprCapability.Model;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Dal.Contracts;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Crud.Helpers;
using Xlent.Lever.Libraries2.Core.Crud.Mappers;
using Xlent.Lever.Libraries2.Core.Storage.Model;

namespace Frobozz.GdprConsent.NexusAdapter.WebApi.Mappers.Logic
{
    /// <inheritdoc />
    public class PersonConsentMapper : ManyToOneBase<PersonConsent, string>
    {
        private readonly IStorage _storage;
        /// <summary>
        /// Constructor
        /// </summary>
        public  PersonConsentMapper(IStorage storage)
        {
            _storage = storage;
        }

        /// <inheritdoc />
        public override async Task<PageEnvelope<PersonConsent>> ReadChildrenWithPagingAsync(string parentId, int offset, int? limit = null,
            CancellationToken token = new CancellationToken())
        {
            var serverId = MapperHelper.MapToType<Guid, string>(parentId);
            var storagePage = await _storage.PersonConsent.ReadChildrenWithPagingAsync(serverId, offset, limit, token);
            FulcrumAssert.IsNotNull(storagePage?.Data);
            var tasks = storagePage?.Data.Select(record => MapFromServerAsync(record, token));
            return new PageEnvelope<PersonConsent>(storagePage?.PageInfo, await Task.WhenAll(tasks));
        }

        /// <summary>
        /// Kept for future use
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task DeleteChildrenAsync(string parentId, CancellationToken token = new CancellationToken())
        {
            var serverId = MapperHelper.MapToType<Guid, string>(parentId);
            await _storage.PersonConsent.DeleteChildrenAsync(serverId, token);
        }

        private async Task<PersonConsent> MapFromServerAsync(PersonConsentTable source,
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

        private PersonConsentTable MapToServer(PersonConsent source)
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
            return target;
        }
    }
}