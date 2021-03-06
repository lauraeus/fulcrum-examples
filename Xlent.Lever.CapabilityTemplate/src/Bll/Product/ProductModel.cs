﻿using System;
using System.Text.RegularExpressions;
using Xlent.Lever.Libraries2.Standard.Assert;
using Xlent.Lever.Libraries2.Standard.Decoupling.Model;
using Xlent.Lever.Libraries2.Standard.Storage.Model;

namespace Xlent.Lever.CapabilityTemplate.Bll.Product
{
    public class ProductModel : StorableItem<IConceptValue>
    {
        public string Category { get; set; }
        public double Price { get; set; }
        public DateTimeOffset DateAdded { get; set; }

        /// <inheritdoc/>
        public override void Validate(string errorLocaction, string propertyPath = "")
        {
            FulcrumValidate.IsNotNull(Id, nameof(Id), errorLocaction);
            FulcrumValidate.IsValidated(Id, propertyPath, nameof(Id), errorLocaction);
            FulcrumValidate.IsNotNullOrWhiteSpace(Name, nameof(Name), errorLocaction);
            FulcrumValidate.IsTrue(Regex.IsMatch(Name, "^[a-zA-Z]+$"), errorLocaction, $"Property {nameof(Name)} must only consist of upper or lower case a-z.");
            FulcrumValidate.IsNotNullOrWhiteSpace(Category, nameof(Category), errorLocaction);
            FulcrumValidate.IsTrue(Regex.IsMatch(Category, "^[a-zA-Z]+$"), errorLocaction, $"Property {nameof(Category)} must only consist of upper or lower case a-z.");
            FulcrumValidate.IsGreaterThanOrEqualTo(0.0, Price, nameof(Price), errorLocaction);
            FulcrumValidate.IsNotDefaultValue(DateAdded, nameof(DateAdded), errorLocaction);
            FulcrumValidate.IsLessThanOrEqualTo(DateTimeOffset.Now, DateAdded, nameof(DateAdded), errorLocaction);
        }
    }
}