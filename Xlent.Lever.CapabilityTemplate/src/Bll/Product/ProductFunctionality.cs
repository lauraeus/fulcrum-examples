using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xlent.Lever.CapabilityTemplate.Bll.Contract.Inbound.Product;
using Xlent.Lever.CapabilityTemplate.Dal.Contract.Product;
using Xlent.Lever.Libraries2.Standard.Assert;
using Xlent.Lever.Libraries2.Standard.Decoupling.Model;
using Xlent.Lever.Libraries2.Standard.Storage.Model;

namespace Xlent.Lever.CapabilityTemplate.Bll.Product
{
    public class ProductFunctionality : IProductFunctionality
    {
        private readonly IProductPersistance _productPersistance;

        public ProductFunctionality(IProductPersistance productPersistance)
        {
            InternalContract.RequireNotNull(productPersistance, nameof(productPersistance));
            _productPersistance = productPersistance;
        }

        public async Task<IPageEnvelope<IProduct, IConceptValue>> ReadAllAsync(int offset = 0, int limit = 100)
        {
            var dalProducts = await _productPersistance.ReadAllAsync().ConfigureAwait(false);

            return dalProducts.Select(FromDal);
        }

        public Task<IProduct> ReadAsync(IConceptValue id)
        {
            InternalContract.RequireNotNull(id, nameof(id));
            InternalContract.RequireValidated(id, nameof(id));
            var dalProduct = await _productPersistance.ReadAsync(ToDal(id)).ConfigureAwait(false);
            return FromDal(dalProduct);
        }

        public async Task<IProduct> CreateAsync(IProduct product)
        {
            InternalContract.RequireNotNull(product, nameof(product));
            InternalContract.RequireValidated(product, nameof(product));

            var dalProduct = await _productPersistance.CreateAsync(ToDal(product)).ConfigureAwait(false);
            return FromDal(dalProduct);
        }

        public async Task<IProduct> UpdateAsync(IProduct product)
        {
            InternalContract.RequireNotNull(product, nameof(product));
            InternalContract.RequireValidated(product, nameof(product));

            var dalProduct = await _productPersistance.UpdateAsync(ToDal(product)).ConfigureAwait(false);
            return FromDal(dalProduct);
        }

        public async Task DeleteAsync(string stringId)
        {
            var isInt = int.TryParse(stringId, out int id);
            InternalContract.Require(isInt, $"Could not parse '{stringId}' as an integer");
            InternalContract.RequireGreaterThan(0, id, nameof(id));
            var dalProduct = await _productPersistance.DeleteAsync(id).ConfigureAwait(false);
            return FromDal(dalProduct);
        }

        public IProduct ProductFactory()
        {
            return new ProductModel();
        }

        public object UnitTest_ToDal(IProduct source)
        {
            return ToDal(source);
        }

        public Task DeleteAsync(IConceptValue id)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public IProduct CreateItemFromFactory()
        {
            throw new System.NotImplementedException();
        }
    }
}