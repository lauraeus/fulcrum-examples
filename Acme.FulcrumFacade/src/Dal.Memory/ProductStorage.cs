using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acme.FulcrumFacade.Dal.Contract.Product;
using Xlent.Lever.Libraries2.Standard.Assert;
using Xlent.Lever.Libraries2.Standard.Error.Logic;

namespace Acme.FulcrumFacade.Dal.Memory
{
    public class ProductStorage : IProductStorage
    {
        private static readonly string Namespace = typeof(ProductStorage).Namespace;
        private IProduct[] _products = {
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1, DateAdded = new DateTime(2016,1,13)},
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75, DateAdded = new DateTime(2015,12,1)},
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99,  DateAdded = new DateTime(2017,1,17)}
          };

        public Task<IEnumerable<IProduct>> GetAllProducts()
        {
            return Task.FromResult<IEnumerable<IProduct>>(_products);
        }

        public Task<IProduct> GetProduct(int id)
        {
            InternalContract.RequireGreaterThan(0, id, nameof(id));
            var product = GetProductOrThrowNotFound(id);
            return Task.FromResult(product);
        }

        public Task<IProduct> CreateProduct(IProduct product)
        {
            InternalContract.RequireNotNull(product, nameof(product));
            InternalContract.RequireValidated(product, nameof(product));

            ThrowConflictIfProductWithIdExists(product.Id);
            return Task.FromResult(product);
        }

        public Task<IProduct> UpdateProduct(IProduct product)
        {
            InternalContract.RequireNotNull(product, nameof(product));
            InternalContract.RequireValidated(product, nameof(product));

            GetProductOrThrowNotFound(product.Id);

            return Task.FromResult(product);
        }

        public Task<IProduct> DeleteProduct(int id)
        {
            InternalContract.RequireGreaterThan(0, id, nameof(id));

            return Task.FromResult(GetProductOrThrowNotFound(id));
        }

        public IProduct ProductFactory()
        {
            return new Product();
        }

        public void Dispose()
        {
            _products = null;
        }

        #region HelperMethods
        private IProduct GetProductOrThrowNotFound(int productId)
        {
            var product = _products.FirstOrDefault(p => p.Id == productId);
            if (product == null) throw new FulcrumNotFoundException($"IProduct with id {productId} could not be found.");
            FulcrumAssert.IsValidated(product, $"{Namespace}: 923656B6-3C2F-4D3C-85AC-A65D5A04D405");
            return product;
        }

        private void ThrowConflictIfProductWithIdExists(int productId)
        {
            var product = _products.FirstOrDefault(p => p.Id == productId);

            if (product != null) throw new FulcrumConflictException($"IProduct with Id: {productId} already exists");
        }
        #endregion
    }
}