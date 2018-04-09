using System;

namespace Xlent.Lever.Libraries2.MoveTo.Core.Translation
{
    public class TranslationConceptAttribute : Attribute
    {
        public string PersonContentId { get; }

        public TranslationConceptAttribute(string personContentId)
        {
            PersonContentId = personContentId;
        }
    }
}