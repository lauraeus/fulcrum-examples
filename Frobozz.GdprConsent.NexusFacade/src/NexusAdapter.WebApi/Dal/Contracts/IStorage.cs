using System;
using Xlent.Lever.Libraries2.Core.Crud.Interfaces;

#pragma warning disable 1591

namespace Frobozz.GdprConsent.NexusAdapter.WebApi.Dal.Contracts
{
    public interface IStorage
    {
        IManyToOneComplete<AddressTable, Guid> Address { get; }
        ICrud<PersonTable, Guid> Person { get; }
        ICrud<ConsentTable, Guid> Consent { get; }
        IManyToOneComplete<PersonConsentTable, Guid> PersonConsent { get; }
    }
}