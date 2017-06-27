using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Xlent.Lever.Libraries2.Standard.Assert;
using Acme.FulcrumFacade.Bll.Contract.Bll.Interface;
using Acme.FulcrumFacade.Sl.WebApi.Model;

namespace Acme.FulcrumFacade.Sl.WebApi.Controllers
{
    /// <summary>
    /// ApiController for Product that does inputcontrol. Logic is separated into another layer. 
    /// </summary>
    [RoutePrefix("api/v1/Products")]
    public class ProductsController : ApiController
    {
        private static readonly string Namespace = typeof(ProductsController).Namespace;
        private readonly IProductLogic _productLogic;

        /// <summary>
        /// Constructor that takes a logic layer for product. 
        /// </summary>
        /// <param name="productLogic">Dependency injected logic layer</param>
        public ProductsController(IProductLogic productLogic)
        {
            _productLogic = productLogic;
        }

        /// <summary>
        /// Retrieves all products that exists.
        /// </summary>
        /// <returns>All products</returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var domainModelProducts = await _productLogic.GetAllProducts();
            var result = AutoMapper.Mapper.Map<IEnumerable<Product>>(domainModelProducts);

            return result;
        }

        /// <summary>
        /// Retrieves a Product that has the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The product with the specified <paramref name="id"/></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("{id}")]
        public async Task<Product> GetProduct(int id)
        {
            ServiceContract.RequireGreaterThan(0, id, nameof(id));

            var domainModelProduct = await _productLogic.GetProduct(id);
            FulcrumAssert.IsValidatedAndNotNull(domainModelProduct, $"{Namespace}: A2AF61A8-A6BC-453C-9F29-5220F8592FEF");
            var result = AutoMapper.Mapper.Map<Product>(domainModelProduct);
            FulcrumAssert.IsValidatedAndNotNull(result, $"{Namespace}: 41042A82-2D71-427F-BBBF-9CDC7545E590");

            return result;
        }

        /// <summary>
        /// Creates a Product with the values specified in <paramref name="product"/>
        /// </summary>
        /// <param name="product"></param>
        /// <returns>The product that was created</returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("")]
        public async Task<Product> CreateProduct(Product product)
        {
            ServiceContract.RequireValidatedAndNotNull(product, nameof(product));

            var domainModelProduct = AutoMapper.Mapper.Map<Bll.Contract.Bll.Model.Product>(product);
            FulcrumAssert.IsValidatedAndNotNull(domainModelProduct, $"{Namespace}: A2AF61A8-A6BC-453C-9F29-5220F8592FEF");
            domainModelProduct = await _productLogic.CreateProduct(domainModelProduct);
            var result = AutoMapper.Mapper.Map<Product>(domainModelProduct);
            FulcrumAssert.IsValidatedAndNotNull(result, $"{Namespace}: 41042A82-2D71-427F-BBBF-9CDC7545E590");

            return result;
        }

        /// <summary>
        /// Updates a Product with the values specified in <paramref name="product"/>
        /// </summary>
        /// <param name="product"></param>
        /// <returns>The product that was updated</returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("")]
        public async Task<Product> UpdateProduct(Product product)
        {
            ServiceContract.RequireValidatedAndNotNull(product, nameof(product));

            var domainModelProduct = AutoMapper.Mapper.Map<Bll.Contract.Bll.Model.Product>(product);
            FulcrumAssert.IsValidatedAndNotNull(domainModelProduct, $"{Namespace}: 8980E968-8C9F-4704-ADF1-00871920F808");
            domainModelProduct = await _productLogic.UpdateProduct(domainModelProduct);
            var result = AutoMapper.Mapper.Map<Product>(domainModelProduct);
            FulcrumAssert.IsValidatedAndNotNull(result, $"{Namespace}: 27A74E83-C31A-4A87-B8C6-1FE5A7FF9F85");

            return result;
        }

        /// <summary>
        /// Deletes the Product with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id of the Product that is supposed to be deleted</param>
        /// <returns>The product that was deleted</returns>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete]
        [Route("{id}")]
        public async Task<Product> DeleteProduct(int id)
        {
            ServiceContract.RequireGreaterThan(0, id, nameof(id));

            var domainModelProduct = await _productLogic.DeleteProduct(id);
            FulcrumAssert.IsValidatedAndNotNull(domainModelProduct, $"{Namespace}: 049D7F2A-A2DF-4CDA-BBD6-3EAD9C883E49");
            var result = AutoMapper.Mapper.Map<Product>(domainModelProduct);
            FulcrumAssert.IsValidatedAndNotNull(result, $"{Namespace}: 8278948E-33CE-4B27-82B1-83BD26CF3788");

            return result;
        }
    }
}
