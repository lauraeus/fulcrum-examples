using System;
using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Frobozz.GdprConsent.Logic.Dal.Contracts;
using Nexus.Link.Libraries.Crud.Mappers;

namespace Frobozz.GdprConsent.Logic.Mappers
{
    /// <summary>
    /// Maps between storage and service models
    /// </summary>
    public class ConsentMapper : 
        CrudMapper<ConsentCreate, Consent, string, ConsentTable, Guid>,
        IConsentService
    {
        /// <inheritdoc />
        public ConsentMapper(IStorage service, IMappable<Consent, ConsentTable> mapper) 
            : base(service.Consent, mapper)
        {
        }
    }
}