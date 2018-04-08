using Xlent.Lever.Libraries2.MoveTo.Core.Translation;

namespace Xlent.Lever.Libraries2.MoveTo.Core.ClientTranslators
{
    public abstract class ClientTranslatorBase
    {
        public string IdConceptName { get; }
        public ITranslatorService TranslatorService { get; }

        // TODO: Read client from context
        public string ClientName => "client.name";

        protected ClientTranslatorBase(string idConceptName, ITranslatorService translatorService)
        {
            IdConceptName = idConceptName;
            TranslatorService = translatorService;
        }

        protected Translator CreateTranslator()
        {
            return new Translator(ClientName, TranslatorService);
        }
    }
}