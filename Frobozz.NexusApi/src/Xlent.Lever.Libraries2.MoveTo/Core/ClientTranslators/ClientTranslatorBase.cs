namespace Xlent.Lever.Libraries2.MoveTo.Core.ClientTranslators
{
    public abstract class ClientTranslatorBase
    {
        public string IdConceptName { get; }

        // TODO: Read client from context
        public string ClientName => "client.name";

        protected ClientTranslatorBase(string idConceptName)
        {
            IdConceptName = idConceptName;
        }
    }
}