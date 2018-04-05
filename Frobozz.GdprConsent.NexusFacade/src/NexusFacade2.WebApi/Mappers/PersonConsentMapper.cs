using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Frobozz.CapabilityContracts.Gdpr;
using Frobozz.GdprConsent.NexusFacade.WebApi.Dal;
using Frobozz.GdprConsent.NexusFacade.WebApi.Dal.Model;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.MoveTo.Core.Mapping;

namespace Frobozz.GdprConsent.NexusFacade.WebApi.Mappers
{
    /// <inheritdoc />
    public class PersonConsentConsentMapper : IMapper<PersonConsent, IStorage, PersonConsentTable>
    {
        /// <inheritdoc />
        public async Task<PersonConsent> CreateAndMapFromServerAsync(PersonConsentTable source, IStorage logic,
            CancellationToken token = default(CancellationToken))
        {
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var target = new PersonConsent
            {
                Id = MapperHelper.MapId<string, Guid>(source.Id),
                Etag = source.Etag,
            };
            FulcrumAssert.IsValidated(target);
            return target;
        }

        /// <inheritdoc />
        public async Task<PersonConsentTable> CreateAndMapToServerAsync(PersonConsent source, IStorage logic, CancellationToken token = default(CancellationToken))
        {
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var id = MapperHelper.MapId<Guid, string>(source.Id);
            var target = new PersonConsentTable
            {
                Id = id,
                Etag = source.Etag,
            };
            FulcrumAssert.IsValidated(target);
            return target;
        }
    }
}