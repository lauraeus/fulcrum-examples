using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Crud.Interfaces;
using Xlent.Lever.Libraries2.Crud.ServerTranslators.To;
using Xlent.Lever.Libraries2.Core.Translation;

namespace Frobozz.NexusApi.Bll.Gdpr.ServerTranslators.To
{
    /// <inheritdoc />
    public class ServerTranslatorTo : IGdprCapability
    {
        /// <inheritdoc />
        public ServerTranslatorTo(IGdprCapability capablity, System.Func<string> getServerNameMethod, ITranslatorService translatorService)
        {
            PersonService = new PersonServerTranslatorTo(capablity, getServerNameMethod, translatorService);
            ConsentService = new ConsentServerTranslatorTo(capablity, getServerNameMethod, translatorService);
            PersonConsentService = new PersonConsentServerTranslatorTo(capablity, getServerNameMethod, translatorService);
            ConsentPersonService = new ConsentPersonServerTranslatorTo(capablity, getServerNameMethod, translatorService);
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