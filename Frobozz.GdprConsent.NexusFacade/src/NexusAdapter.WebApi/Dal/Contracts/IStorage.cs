using System;
using Xlent.Lever.Libraries2.Crud.Interfaces;

#pragma warning disable 1591

namespace Frobozz.GdprConsent.NexusAdapter.WebApi.Dal.Contracts
{
    public interface IStorage
    {
        ICrudManyToOne<AddressTable, Guid> Address { get; }
        ICrud<PersonTable, Guid> Person { get; }
        ICrud<ConsentTable, Guid> Consent { get; }
        ICrudManyToMany<PersonConsentTable, PersonTable, ConsentTable, Guid> PersonConsent { get; }
    }
}