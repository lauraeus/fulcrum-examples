using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Crud.Interfaces;
using Xlent.Lever.Libraries2.Crud.ServerTranslators.From;

namespace Frobozz.NexusApi.Bll.Gdpr.ServerTranslators.From
{
    /// <inheritdoc />
    public class ServerTranslatorFrom : IGdprCapability
    {
        /// <inheritdoc />
        public ServerTranslatorFrom(IGdprCapability capablity, System.Func<string> getServerNameMethod)
        {
            PersonService = new PersonServerTranslatorFrom(capablity, getServerNameMethod);
            ConsentService = new ConsentServerTranslatorFrom(capablity, getServerNameMethod);
            PersonConsentService = new PersonConsentServerTranslatorFrom(capablity, getServerNameMethod);
            ConsentPersonService = new ConsentPersonServerTranslatorFrom(capablity, getServerNameMethod);
        }

        /// <inheritdoc />
        public IPersonService PersonService { get; }

        /// <inheritdoc />
        public IConsentService ConsentService { get; }

        /// <inheritdoc />
        public IPersonConsentService PersonConsentService { get; }

        /// <inheritdoc />
        public IConsentPersonService ConsentPersonService { get; }
    }
}