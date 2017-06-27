using System;
using System.Collections.Generic;

namespace Sl.WebApi.Tests.Product
{
    internal static class ProductFactory
    {
        public static IList<Acme.FulcrumFacade.Sl.WebApi.Model.Product> CreateProducts(int numberOfProducts)
        {
            IList<Acme.FulcrumFacade.Sl.WebApi.Model.Product> products = new List<Acme.FulcrumFacade.Sl.WebApi.Model.Product>();
            for (var i = 0; i < numberOfProducts; ++i)
            {
                products.Add(CreateProduct($"{i + 1}"));
            }
            return products;
        }

        public static Acme.FulcrumFacade.Sl.WebApi.Model.Product CreateProduct(string id = "1", string name = "mockName", string category = "mockCategory", double price = 123)
        {
            return new Acme.FulcrumFacade.Sl.WebApi.Model.Product
            {
                Id = id,
                Name = name,
                Category = category,
                Price = price,
                DateAdded = new DateTime(2017, 02, 22)
            };
        }
    }
}
