using System;
using Xlent.Lever.Libraries2.Standard.Decoupling.Model;
using Xlent.Lever.Libraries2.Standard.Storage.Model;

namespace Xlent.Lever.CapabilityTemplate.Bll.Contract.Inbound.Product
{
    /// <summary>
    /// A product that can be sold
    /// </summary>
    public interface IProduct : IStorableItemRecommended<IConceptValue>
    {
        /// <summary>
        /// The product category
        /// </summary>
        string Category { get; set; }

        /// <summary>
        /// The price of the product, in SEK
        /// </summary>
        double Price { get; set; }

        /// <summary>
        /// The time that the product was added to our product catalog.
        /// </summary>
        DateTimeOffset DateAdded { get; set; }
    }
}