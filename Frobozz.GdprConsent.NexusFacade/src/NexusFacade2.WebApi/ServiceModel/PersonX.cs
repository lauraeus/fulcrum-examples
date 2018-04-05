using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Frobozz.CapabilityContracts.Gdpr;
using Frobozz.GdprConsent.NexusFacade.WebApi.Dal;
using Frobozz.GdprConsent.NexusFacade.WebApi.Dal.Model;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.MoveTo.Core.Mapping;

namespace Frobozz.GdprConsent.NexusFacade.WebApi.ServiceModel
{
    /// <inheritdoc />
    public class Personx : Person, IMapper<PersonTable, IStorage>
    {
        /// <inheritdoc />
        public async Task MapFromAsync(PersonTable source, IStorage storage, CancellationToken token = default(CancellationToken))
        {
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var addressesDbTask = storage.Address.ReadChildrenAsync(source.Id, token: token);
            Id = source.Id.ToString();
            Name = source.Name;
            Etag = source.Etag;
            Addresses = (await addressesDbTask).Select(db => ToService(db));
            FulcrumAssert.IsValidated(this);
        }

        /// <inheritdoc />
        public Task<PersonTable> CreateAndMapToAsync(IStorage logic, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }
}