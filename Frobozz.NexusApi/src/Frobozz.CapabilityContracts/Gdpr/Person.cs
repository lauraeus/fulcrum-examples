using System.Collections.Generic;
using Frobozz.CapabilityContracts.Core.Translation;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Storage.Model;

namespace Frobozz.CapabilityContracts.Gdpr
{
    public class Person : StorableItem<string>, ITranslatable
    {
        /// <summary>
        /// The name of the person
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The addresses for this person.
        /// </summary>
        public IEnumerable<Address> Addresses { get; set; }

        /// <inheritdoc />
        public override void Validate(string errorLocation, string propertyPath = "")
        {
           FulcrumValidate.IsNotNullOrWhiteSpace(Name, nameof(Name), errorLocation);
        }
        public void DecorateForTranslation(string clientName)
        {
            Id = TranslationHelper.Decorate("person.id", clientName, Id);
            foreach (var address in Addresses)
            {
                address.DecorateForTranslation(clientName);
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Name}";
        }
    }
}