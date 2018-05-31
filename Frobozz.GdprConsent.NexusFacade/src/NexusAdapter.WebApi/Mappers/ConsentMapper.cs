using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Dal.Contracts;
using Xlent.Lever.Libraries2.Crud.Interfaces;
using Xlent.Lever.Libraries2.Crud.Mappers;

namespace Frobozz.GdprConsent.NexusAdapter.WebApi.Mappers
{
    /// <summary>
    /// Maps between storage and service models
    /// </summary>
    public class ConsentMapper : 
        CrudMapper<ConsentCreate, Consent, string, ConsentTable, Guid>,
        IConsentService
    {
        /// <inheritdoc />
        public ConsentMapper(IStorage service, IMappable mapper) 
            : base(service.Consent, mapper)
        {
        }
    }
}