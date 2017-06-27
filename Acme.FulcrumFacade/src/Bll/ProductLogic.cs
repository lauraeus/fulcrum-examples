using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xlent.Lever.Libraries2.Standard.Assert;
using Acme.FulcrumFacade.Bll.Contract.Bll.Interface;
using Acme.FulcrumFacade.Bll.Contract.Bll.Model;
using Acme.FulcrumFacade.Bll.Contract.Dal;

namespace Acme.FulcrumFacade.Bll
{
    public class ProductLogic : IProductLogic
    {
        private readonly IProductRepository _productRepository;

        public ProductLogic(IProductRepository productRepository)
        {
            InternalContract.RequireNotNull(productRepository, nameof(productRepository));
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _productRepository.GetAllProducts().ConfigureAwait(false);
        }

        public async Task<Product> GetProduct(int id)
        {
            InternalContract.RequireGreaterThan(0, id, nameof(id));
            var product = await _productRepository.GetProduct(id).ConfigureAwait(false);
            return product;
        }

        public async Task<Product> CreateProduct(Product product)
        {
            InternalContract.RequireValidatedAndNotNull(product, nameof(product));

            return await _productRepository.CreateProduct(product).ConfigureAwait(false);
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            InternalContract.RequireValidatedAndNotNull(product, nameof(product));

            return await _productRepository.UpdateProduct(product).ConfigureAwait(false);
        }

        public async Task<Product> DeleteProduct(int id)
        {
            InternalContract.RequireGreaterThan(0, id, nameof(id));
            return await _productRepository.DeleteProduct(id).ConfigureAwait(false);
        }
    }
}