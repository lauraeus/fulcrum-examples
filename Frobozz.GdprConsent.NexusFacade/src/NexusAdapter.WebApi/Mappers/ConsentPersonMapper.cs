using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Dal.Contracts;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Storage.Model;
using Xlent.Lever.Libraries2.Crud.Helpers;
using Xlent.Lever.Libraries2.Crud.Mappers;

namespace Frobozz.GdprConsent.NexusAdapter.WebApi.Mappers
{
    /// <summary>
    /// Map between storage and service
    /// </summary>
    public class ConsentPersonMapper : IConsentPersonService
    {
        private readonly IStorage _storage;

        /// <summary>
        /// Constructor
        /// </summary>
        public ConsentPersonMapper(IStorage storage)
        {
            _storage = storage;
        }

        /// <inheritdoc />
        public async Task<PageEnvelope<PersonConsent>> ReadChildrenWithPagingAsync(string consentId, int offset, int? limit = null,
            CancellationToken token = new CancellationToken())
        {
            var serverId = MapperHelper.MapToType<Guid, string>(consentId);
            var storagePage = await _storage.PersonConsent.ReadByReference2WithPagingAsync(serverId, offset, limit, token);
            FulcrumAssert.IsNotNull(storagePage?.Data);
            var tasks = storagePage?.Data.Select(record => MapFromServerAsync(record, token));
            return new PageEnvelope<PersonConsent>(storagePage?.PageInfo, await Task.WhenAll(tasks));
        }

        private async Task<PersonConsent> MapFromServerAsync(PersonConsentTable source,
            CancellationToken token = default(CancellationToken))
        {
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var serverConsentTask = _storage.Consent.ReadAsync(source.ConsentId, token);
            var serverPersonTask =  _storage.Consent.ReadAsync(source.PersonId, token);
            var target = new PersonConsent
            {
                Id = MapperHelper.MapToType<string, Guid>(source.Id),
                ConsentId = MapperHelper.MapToType<string, Guid>(source.ConsentId),
                PersonName = (await serverPersonTask).Name,
                ConsentName = (await serverConsentTask).Name,
                Etag = source.Etag,
                PersonId = MapperHelper.MapToType<string, Guid>(source.PersonId),
                HasGivenConsent = source.HasGivenConsent
            };
            FulcrumAssert.IsValidated(target);
            return target;
        }
    }
}