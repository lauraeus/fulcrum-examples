using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xlent.Lever.CapabilityTemplate.Dal.Contract.Product;
using Xlent.Lever.Libraries2.Standard.Assert;
using Xlent.Lever.Libraries2.Standard.Error.Logic;

namespace Xlent.Lever.CapabilityTemplate.Dal.Memory
{
    public class ProductPersistance : IProductPersistance
    {
        private static readonly string Namespace = typeof(ProductPersistance).Namespace;
        private IStorableProduct[] _storableProducts = {
            new StorableProduct { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1, DateAdded = new DateTime(2016,1,13)},
            new StorableProduct { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75, DateAdded = new DateTime(2015,12,1)},
            new StorableProduct { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99,  DateAdded = new DateTime(2017,1,17)}
          };

        public Task<IEnumerable<IStorableProduct>> ReadAllAsync()
        {
            return Task.FromResult<IEnumerable<IStorableProduct>>(_storableProducts);
        }

        public Task<IStorableProduct> ReadAsync(int id)
        {
            InternalContract.RequireGreaterThan(0, id, nameof(id));
            var product = GetProductOrThrowNotFound(id);
            return Task.FromResult(product);
        }

        public Task<IStorableProduct> CreateAsync(IStorableProduct storableProduct)
        {
            InternalContract.RequireNotNull(storableProduct, nameof(storableProduct));
            InternalContract.RequireValidated(storableProduct, nameof(storableProduct));

            ThrowConflictIfProductWithIdExists(storableProduct.Id);
            return Task.FromResult(storableProduct);
        }

        public Task<IStorableProduct> UpdateAsync(IStorableProduct storableProduct)
        {
            InternalContract.RequireNotNull(storableProduct, nameof(storableProduct));
            InternalContract.RequireValidated(storableProduct, nameof(storableProduct));

            GetProductOrThrowNotFound(storableProduct.Id);

            return Task.FromResult(storableProduct);
        }

        public Task<IStorableProduct> DeleteAsync(int id)
        {
            InternalContract.RequireGreaterThan(0, id, nameof(id));

            return Task.FromResult(GetProductOrThrowNotFound(id));
        }

        public IStorableProduct ProductFactory()
        {
            return new StorableProduct();
        }

        public void Dispose()
        {
            _storableProducts = null;
        }

        #region HelperMethods
        private IStorableProduct GetProductOrThrowNotFound(int productId)
        {
            var product = _storableProducts.FirstOrDefault(p => p.Id == productId);
            if (product == null) throw new FulcrumNotFoundException($"IStorableProduct with id {productId} could not be found.");
            FulcrumAssert.IsValidated(product, $"{Namespace}: 923656B6-3C2F-4D3C-85AC-A65D5A04D405");
            return product;
        }

        private void ThrowConflictIfProductWithIdExists(int productId)
        {
            var product = _storableProducts.FirstOrDefault(p => p.Id == productId);

            if (product != null) throw new FulcrumConflictException($"IStorableProduct with Id: {productId} already exists");
        }
        #endregion
    }
}