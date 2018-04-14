using System;
using System.Threading;
using System.Threading.Tasks;
using Frobozz.CapabilityContracts.Gdpr.Model;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Contracts;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Crud.Mapping;

namespace Frobozz.GdprConsent.NexusAdapter.WebApi.Mappers.Model
{
    /// <inheritdoc />
    public class PersonConsentModelMapper : IModelMapper<PersonConsent, IServerLogic, PersonConsentTable>
    {
        /// <inheritdoc />
        public async Task<PersonConsent> MapFromServerAsync(PersonConsentTable source, IServerLogic logic,
            CancellationToken token = default(CancellationToken))
        {
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var serverConsent = await logic.Consent.ReadAsync(source.ConsentId, token);
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
        public async Task<PersonConsentTable> MapToServerAsync(PersonConsent source, IServerLogic logic, CancellationToken token = default(CancellationToken))
        {
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var target = MapToServer(source);
            return await Task.FromResult(target);
        }

        /// <inheritdoc />
        public PersonConsentTable MapToServer(PersonConsent source)
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