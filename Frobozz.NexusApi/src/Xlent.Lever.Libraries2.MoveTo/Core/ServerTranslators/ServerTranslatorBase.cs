using Xlent.Lever.Libraries2.MoveTo.Core.Translation;

namespace Xlent.Lever.Libraries2.MoveTo.Core.ServerTranslators
{
    public abstract class ServerTranslatorBase
    {
        public string IdConceptName { get; }
        public ITranslatorService TranslatorService { get; }

        // TODO: Read server from context
        public string ServerName => "server.name";

        protected ServerTranslatorBase(string idConceptName, ITranslatorService translatorService)
        {
            IdConceptName = idConceptName;
            TranslatorService = translatorService;
        }

        protected Translator CreateTranslator()
        {
            return new Translator(ServerName, TranslatorService);
        }
    }
}