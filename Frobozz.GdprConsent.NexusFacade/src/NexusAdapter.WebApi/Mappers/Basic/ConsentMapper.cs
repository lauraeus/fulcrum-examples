using System;
using Frobozz.Contracts.GdprCapability.Model;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Dal.Contracts;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Crud.Helpers;
using Xlent.Lever.Libraries2.Crud.Mappers;

namespace Frobozz.GdprConsent.NexusAdapter.WebApi.Mappers.Basic
{
    /// <inheritdoc />
    public class ConsentMapper : IMapper<ConsentCreate, Consent, ConsentTable>
    {
        /// <inheritdoc />
        public Consent MapFromServer(ConsentTable source)
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
        public ConsentTable MapToServer(ConsentCreate source)
        {
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var target = new ConsentTable()
            {
                Name = source.Name
            };
            FulcrumAssert.IsValidated(target);
            return target;
        }

        /// <inheritdoc />
        public ConsentTable MapToServer(Consent source)
        {
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var id = MapperHelper.MapToType<Guid, string>(source.Id);
            var target = MapToServer(source as ConsentCreate);
            target.Id = id;
            target.Etag = source.Etag;
            FulcrumAssert.IsValidated(target);
            return target;
        }
    }
}