using System;
using System.Threading;
using System.Threading.Tasks;
using Frobozz.CapabilityContracts.Gdpr.Model;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Contracts;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.MoveTo.Core.Mapping;

namespace Frobozz.GdprConsent.NexusAdapter.WebApi.Mappers.Model
{
    /// <inheritdoc />
    public class PersonConsentModelMapper : IModelMapper<PersonConsent, IServerLogic, PersonConsentTable>
    {
        /// <inheritdoc />
        public async Task<PersonConsent> CreateAndMapFromServerAsync(PersonConsentTable source, IServerLogic logic,
            CancellationToken token = default(CancellationToken))
        {
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var serverConsent = await logic.Consent.ReadAsync(source.ConsentId, token);
            var target = new PersonConsent
            {
                Id = MapperHelper.MapId<string, Guid>(source.Id),
                ConsentId = MapperHelper.MapId<string, Guid>(source.ConsentId),
                ConsentName = serverConsent.Name,
                Etag = source.Etag,
                PersonId = MapperHelper.MapId<string, Guid>(source.PersonId),
                HasGivenConsent = source.HasGivenConsent
            };
            FulcrumAssert.IsValidated(target);
            return target;
        }

        /// <inheritdoc />
        public async Task<PersonConsentTable> CreateAndMapToServerAsync(PersonConsent source, IServerLogic logic, CancellationToken token = default(CancellationToken))
        {
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var target = new PersonConsentTable
            {
                Id = MapperHelper.MapId<Guid, string>(source.Id),
                Etag = source.Etag,
                HasGivenConsent = source.HasGivenConsent,
                PersonId = MapperHelper.MapId<Guid, string>(source.PersonId),
                ConsentId = MapperHelper.MapId<Guid, string>(source.ConsentId),
            };
            FulcrumAssert.IsValidated(target);
            return await Task.FromResult(target);
        }
    }
}