using System;
using Frobozz.Contracts.GdprCapability.Model;
using Frobozz.GdprConsent.Logic.Dal.Contracts;
using Nexus.Link.Libraries.Core.Assert;
using Nexus.Link.Libraries.Crud.Helpers;
using Nexus.Link.Libraries.Crud.Mappers;

namespace Frobozz.GdprConsent.Logic.Mappers.Basic
{
    /// <inheritdoc />
    public class PersonConsentMapper : IReadMapper<PersonConsent, PersonConsentTable>
    {
        /// <inheritdoc />
        public PersonConsent MapFromServer(PersonConsentTable source)
        {
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var target = new PersonConsent
            {
                Id = MapperHelper.MapToType<string, Guid>(source.Id),
                ConsentId = MapperHelper.MapToType<string, Guid>(source.ConsentId),
                PersonId = MapperHelper.MapToType<string, Guid>(source.PersonId),
                Etag = source.Etag
            };
            FulcrumAssert.IsValidated(target);
            return target;
        }
    }
}