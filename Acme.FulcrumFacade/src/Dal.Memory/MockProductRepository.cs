using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acme.FulcrumFacade.Bll.Contract.Bll.Model;
using Acme.FulcrumFacade.Bll.Contract.Dal;
using Xlent.Lever.Libraries2.Standard.Assert;
using Xlent.Lever.Libraries2.Standard.Error.Logic;

namespace Acme.FulcrumFacade.Dal.Memory
{
    public class MockProductRepository : IProductRepository
    {
        private static readonly string Namespace = typeof(MockProductRepository).Namespace;
        private Product[] _products = {
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1, DateAdded = new DateTime(2016,1,13)},
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75, DateAdded = new DateTime(2015,12,1)},
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99,  DateAdded = new DateTime(2017,1,17)}
          };

        public Task<IEnumerable<Product>> GetAllProducts()
        {
            return Task.FromResult<IEnumerable<Product>>(_products);
        }

        public Task<Product> GetProduct(int id)
        {
            InternalContract.RequireGreaterThan(0, id, nameof(id));
            var product = GetProductOrThrowNotFound(id);
            return Task.FromResult(product);
        }

        public Task<Product> CreateProduct(Product product)
        {
            InternalContract.RequireNotNull(product, nameof(product));
            InternalContract.RequireValidated(product, nameof(product));

            ThrowConflictIfProductWithIdExists(product.Id);
            return Task.FromResult(product);
        }

        public Task<Product> UpdateProduct(Product product)
        {
            InternalContract.RequireNotNull(product, nameof(product));
            InternalContract.RequireValidated(product, nameof(product));

            GetProductOrThrowNotFound(product.Id);

            return Task.FromResult(product);
        }

        public Task<Product> DeleteProduct(int id)
        {
            InternalContract.RequireGreaterThan(0, id, nameof(id));

            return Task.FromResult(GetProductOrThrowNotFound(id));
        }

        public void Dispose()
        {
            _products = null;
        }

        #region HelperMethods
        private Product GetProductOrThrowNotFound(int productId)
        {
            var product = _products.FirstOrDefault(p => p.Id == productId);
            if (product == null) throw new FulcrumNotFoundException($"Product with id {productId} could not be found.");
            FulcrumAssert.IsValidated(product, $"{Namespace}: 923656B6-3C2F-4D3C-85AC-A65D5A04D405");
            return product;
        }

        private void ThrowConflictIfProductWithIdExists(int productId)
        {
            var product = _products.FirstOrDefault(p => p.Id == productId);

            if (product != null) throw new FulcrumConflictException($"Product with Id: {productId} already exists");
        }
        #endregion
    }
}