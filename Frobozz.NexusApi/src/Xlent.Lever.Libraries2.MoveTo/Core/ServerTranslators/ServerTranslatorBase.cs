namespace Xlent.Lever.Libraries2.MoveTo.Core.ServerTranslators
{
    public abstract class ServerTranslatorBase
    {
        public string IdConceptName { get; }

        // TODO: Read client from context
        public string ServerName => "server.name";

        protected ServerTranslatorBase(string idConceptName)
        {
            IdConceptName = idConceptName;
        }
    }
}