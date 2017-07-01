using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acme.FulcrumFacade.Dal.Contract.Product;
using Xlent.Lever.Libraries2.Standard.Assert;
using IProduct = Acme.FulcrumFacade.Bll.Contract.Product.IProduct;

namespace Acme.FulcrumFacade.Bll.Product
{
    public class ProductLogic : Contract.Product.IProductLogic
    {
        private readonly IProductStorage _productStorage;

        public ProductLogic(IProductStorage productStorage)
        {
            InternalContract.RequireNotNull(productStorage, nameof(productStorage));
            _productStorage = productStorage;
        }

        public async Task<IEnumerable<IProduct>> GetAllProducts()
        {
            var dalProducts = await _productStorage.GetAllProducts().ConfigureAwait(false);
            return dalProducts.Select(FromDal);
        }

        public async Task<IProduct> GetProduct(string stringId)
        {
            var isInt = int.TryParse(stringId, out int id);
            InternalContract.Require(isInt, $"Could not parse '{stringId}' as an integer");
            InternalContract.RequireGreaterThan(0, id, nameof(id));
            var dalProduct = await _productStorage.GetProduct(id).ConfigureAwait(false);
            return FromDal(dalProduct);
        }

        public async Task<IProduct> CreateProduct(IProduct product)
        {
            InternalContract.RequireNotNull(product, nameof(product));
            InternalContract.RequireValidated(product, nameof(product));

            var dalProduct = await _productStorage.CreateProduct(ToDal(product)).ConfigureAwait(false);
            return FromDal(dalProduct);
        }

        public async Task<IProduct> UpdateProduct(IProduct product)
        {
            InternalContract.RequireNotNull(product, nameof(product));
            InternalContract.RequireValidated(product, nameof(product));

            var dalProduct = await _productStorage.UpdateProduct(ToDal(product)).ConfigureAwait(false);
            return FromDal(dalProduct);
        }

        public async Task<IProduct> DeleteProduct(string stringId)
        {
            var isInt = int.TryParse(stringId, out int id);
            InternalContract.Require(isInt, $"Could not parse '{stringId}' as an integer");
            InternalContract.RequireGreaterThan(0, id, nameof(id));
            var dalProduct = await _productStorage.DeleteProduct(id).ConfigureAwait(false);
            return FromDal(dalProduct);
        }

        public IProduct ProductFactory()
        {
            return new Product();
        }

        public object UnitTest_ToDal(IProduct source)
        {
            return ToDal(source);
        }

        internal Dal.Contract.Product.IProduct ToDal(IProduct source)
        {

            if (source == null) return null;
            var target = _productStorage.ProductFactory();
            target.Id = source.Id;
            target.Name = source.Name;
            target.Category = source.Category;
            target.DateAdded = source.DateAdded;
            target.Price = source.Price;
            return target;
        }

        internal IProduct FromDal(Dal.Contract.Product.IProduct source)
        {
            if (source == null) return null;
            // ReSharper disable once UseObjectOrCollectionInitializer
            var target = new Product();
            target.Id = source.Id;
            target.Name = source.Name;
            target.Category = source.Category;
            target.DateAdded = source.DateAdded;
            target.Price = source.Price;
            return target;
        }
    }
}