using System;
using Xlent.Lever.Libraries2.Standard.Storage.Model;

namespace Acme.FulcrumFacade.Dal.Contract.Product
{
    /// <summary>
    /// How a product is stored
    /// </summary>
    public interface IStorableProduct : IStorableItemRecommended<int>
    {
        /// <summary>
        /// The category of a product
        /// </summary>
        string Category { get; set; }
        /// <summary>
        /// The time that the product was added to storage
        /// </summary>
        DateTimeOffset DateAdded { get; set; }

        /// <summary>
        /// The price of the product, in SEK.
        /// </summary>
        double Price { get; set; }
    }
}