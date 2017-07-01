using System;
using Acme.FulcrumFacade.Dal.Contract.Product;

namespace Acme.FulcrumFacade.Bll.Tests.Product
{
    public class StorableProductMock : IStorableProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public DateTimeOffset DateAdded { get; set; }

        /// <inheritdoc/>
        public void Validate(string errorLocaction, string propertyPath = "")
        {
        }
    }
}