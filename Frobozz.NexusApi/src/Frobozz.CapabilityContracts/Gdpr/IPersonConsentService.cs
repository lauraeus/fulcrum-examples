﻿using Xlent.Lever.Libraries2.Core.Storage.Model;

namespace Frobozz.CapabilityContracts.Gdpr
{
    public interface IPersonConsentService : IManyToOneRelation<PersonConsent, string>
    {
    }
}