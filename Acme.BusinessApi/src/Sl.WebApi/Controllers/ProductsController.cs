using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Acme.FulcrumFacade.Bll.Contract.Inbound.Product;
using Xlent.Lever.Libraries2.Standard.Assert;
using Xlent.Lever.Libraries2.Standard.Storage.Model;
using Product = Acme.FulcrumFacade.Sl.WebApi.Model.Product;

namespace Acme.FulcrumFacade.Sl.WebApi.Controllers
{
    /// <summary>
    /// ApiController for Product that does inputcontrol. Logic is separated into another layer. 
    /// </summary>
    [RoutePrefix("api/v1/Products")]
    public class ProductsController : ApiController
    {
        private static readonly string Namespace = typeof(ProductsController).Namespace;
        private readonly IProductFunctionality _productFunctionality;

        /// <summary>
        /// Constructor that takes a logic layer for product. 
        /// </summary>
        /// <param name="productFunctionality">Dependency injected logic layer</param>
        public ProductsController(IProductFunctionality productFunctionality)
        {
            _productFunctionality = productFunctionality;
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
        public async Task<PageEnvelope<Product, string>> GetAllAsync()
        {
            var bllEnvelope = await _productFunctionality.ReadAllAsync();

            var slEnvelope = new PageEnvelope<int>
            {
                PageInfo = bllEnvelope.PageInfo,
                Data = bllEnvelope.Data.Select(FromBll)
            };
            return slEnvelope;
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
        public async Task<Product> GetProduct(string id)
        {
            ServiceContract.RequireNotNullOrWhitespace(id, nameof(id));

            var bllProduct = await _productFunctionality.ReadAsync(id);
            var result = FromBll(bllProduct);
            FulcrumAssert.IsNotNull(result, nameof(result));
            FulcrumAssert.IsValidated(result, $"{Namespace}: 41042A82-2D71-427F-BBBF-9CDC7545E590");
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
            ServiceContract.RequireNotNull(product, nameof(product));
            ServiceContract.RequireValidated(product, nameof(product));

            var bllProduct = ToBll(product);
            bllProduct = await _productFunctionality.CreateAsync(bllProduct);

            var result = FromBll(bllProduct);
            FulcrumAssert.IsNotNull(result, nameof(result));
            FulcrumAssert.IsValidated(result, $"{Namespace}: 41042A82-2D71-427F-BBBF-9CDC7545E590");

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
            ServiceContract.RequireNotNull(product, nameof(product));
            ServiceContract.RequireValidated(product, nameof(product));

            var bllProduct = ToBll(product);
            bllProduct = await _productFunctionality.Update(bllProduct);

            var result = FromBll(bllProduct);
            FulcrumAssert.IsNotNull(result, nameof(result));
            FulcrumAssert.IsValidated(result, $"{Namespace}: 27A74E83-C31A-4A87-B8C6-1FE5A7FF9F85");

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
        public async Task<Product> DeleteProduct(string id)
        {
            ServiceContract.RequireNotNullOrWhitespace(id, nameof(id));

            var bllProduct = await _productFunctionality.Delete(id);

            var result = FromBll(bllProduct);
            FulcrumAssert.IsNotNull(result, nameof(result));
            FulcrumAssert.IsValidated(result, $"{Namespace}: 8278948E-33CE-4B27-82B1-83BD26CF3788");

            return result;
        }

        /// <summary>
        /// This is for testing purposes only. Converts a product object from Service contract to Business Logic contrace
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public object UnitTest_ToBll(Product source)
        {
            return ToBll(source);
        }

        internal IProduct ToBll(Product source)
        {

            if (source == null) return null;
            var target = _productFunctionality.ProductFactory();
            target.Id = int.Parse(source.Id);
            target.Name = source.Name;
            target.Category = source.Category;
            target.DateAdded = source.DateAdded;
            target.Price = source.Price;
            return target;
        }

        internal Product FromBll(IProduct source)
        {
            if (source == null) return null;
#pragma warning disable IDE0017 // Simplify object initialization
            // ReSharper disable once UseObjectOrCollectionInitializer
            var target = new Product();
#pragma warning restore IDE0017 // Simplify object initialization
            target.Id = source.Id.ToString();
            target.Name = source.Name;
            target.Category = source.Category;
            target.DateAdded = source.DateAdded;
            target.Price = source.Price;
            return target;
        }
    }
}
