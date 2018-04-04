using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Frobozz.CapabilityContracts.Gdpr;
using Frobozz.GdprConsent.NexusFacade.WebApi.Dal;
using Frobozz.GdprConsent.NexusFacade.WebApi.Dal.Model;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.MoveTo.Core.Mapping;

namespace Frobozz.GdprConsent.NexusFacade.WebApi.ServiceModel
{
    /// <inheritdoc />
    public class PersonX : Person, IMapper<PersonTable, IStorage>
    {
        /// <inheritdoc />
        public async Task MapFromAsync(PersonTable source, IStorage storage)
        {
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var addressesDbTask = storage.Address.ReadChildrenAsync(source.Id);
            var target = new Person()
            {
                Id = source.Id.ToString(),
                Name = source.Name,
                Etag = source.Etag,
                Addresses = (await addressesDbTask).Select(db => ToService(db))
            };
            FulcrumAssert.IsValidated(target);
            return target;
        }

        /// <inheritdoc />
        public async Task<PersonTable> CreateAndMapTo(IStorage logic)
        {
            throw new NotImplementedException();
        }
    }
}