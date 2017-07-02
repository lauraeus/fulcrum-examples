using System;
using Xlent.Lever.CapabilityTemplate.Bll.Contract.Inbound.Product;

namespace Xlent.Lever.CapabilityTemplate.Sl.WebApi.Tests.Product
{
    public class ProductMock : IProduct
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