using System;
using Nexus.Link.Libraries.Crud.Interfaces;

#pragma warning disable 1591

namespace Frobozz.GdprConsent.Logic.Dal.Contracts
{
    public interface IStorage
    {
        ICrudManyToOne<AddressTable, Guid> Address { get; }
        ICrud<PersonTable, Guid> Person { get; }
        ICrud<ConsentTable, Guid> Consent { get; }
        ICrudManyToMany<PersonConsentTable, PersonTable, ConsentTable, Guid> PersonConsent { get; }
    }
}