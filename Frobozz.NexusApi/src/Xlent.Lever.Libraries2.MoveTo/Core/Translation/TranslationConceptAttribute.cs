using System;

namespace Xlent.Lever.Libraries2.MoveTo.Core.Translation
{
    public class TranslationConceptAttribute : Attribute
    {
        public string ConceptName { get; }

        public TranslationConceptAttribute(string conceptName)
        {
            ConceptName = conceptName;
        }
    }
}