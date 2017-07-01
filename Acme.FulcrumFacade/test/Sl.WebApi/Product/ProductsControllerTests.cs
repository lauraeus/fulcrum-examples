using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acme.FulcrumFacade.Bll.Contract.Product;
using Acme.FulcrumFacade.Sl.WebApi.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Acme.FulcrumFacade.Sl.WebApi.Tests.Product
{
    [TestClass]
    public class ProductsControllerTests
    {
        private ProductsController _controller;
        private Mock<IProductLogic> _productLogic;
        private Acme.FulcrumFacade.Sl.WebApi.Model.Product _slExpectedProduct;
        private IProduct _bllExpectedProduct;

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
        }

        [TestInitialize]
        public void Initialize()
        {
           
            _productLogic = new Mock<IProductLogic>();
            _productLogic.Setup(mock => mock.ProductFactory()).Returns(new ProductMock());
            _controller = new ProductsController(_productLogic.Object);

            _slExpectedProduct = ProductFactory.CreateProduct();
            _bllExpectedProduct = ToBll(_slExpectedProduct);
        }

        private IProduct ToBll(Model.Product source)
        {
            return (IProduct) _controller.UnitTest_ToBll(source);
        }

        #region GetProduct()
        [TestMethod]
        public async Task GetProduct()
        {
            _productLogic.Setup(mock => mock.GetProduct(_bllExpectedProduct.Id.ToString())).ReturnsAsync(_bllExpectedProduct);
            var actualProduct = await _controller.GetProduct(_slExpectedProduct.Id);
            AssertProductsAreEqual(_slExpectedProduct, actualProduct);
        }
        #endregion

        #region GetAllProduct()
        [TestMethod]
        public async Task GetAllProducts()
        {
            var expectedProducts = ProductFactory.CreateProducts(3);
            var bllExpectedProducts = expectedProducts.Select(ToBll);
            _productLogic.Setup(mockRepo => mockRepo.GetAllProducts()).ReturnsAsync(bllExpectedProducts);
            var actualProducts = await _controller.GetAllProducts();
            AssertProductsAreEqual(expectedProducts.ToList(), actualProducts.ToList());
        }
        #endregion

        #region UpdateProduct()
        [TestMethod]
        public async Task UpdateProduct()
        {
            _productLogic.Setup(mockRepo => mockRepo.UpdateProduct(It.IsNotNull<IProduct>())).ReturnsAsync(_bllExpectedProduct);
            _slExpectedProduct.Validate("err loc");
            var actualProduct = await _controller.UpdateProduct(_slExpectedProduct);

            AssertProductsAreEqual(_slExpectedProduct, actualProduct);
        }
        #endregion

        #region CreateProduct()
        [TestMethod]
        public async Task CreateProduct()
        {
            _productLogic.Setup(mockRepo => mockRepo.CreateProduct(It.IsNotNull<IProduct>())).ReturnsAsync(_bllExpectedProduct);
            _slExpectedProduct.Validate("err loc");
            var actualProduct = await _controller.CreateProduct(_slExpectedProduct);

            AssertProductsAreEqual(_slExpectedProduct, actualProduct);
        }
        #endregion

        #region DeleteProduct
        [TestMethod]
        public async Task DeleteProduct()
        {
            _productLogic.Setup(mock => mock.DeleteProduct(_bllExpectedProduct.Id.ToString())).ReturnsAsync(_bllExpectedProduct);
            var actualProduct = await _controller.DeleteProduct(_slExpectedProduct.Id);
            AssertProductsAreEqual(_slExpectedProduct, actualProduct);
        }
        #endregion

        #region AssertionMethods

        private void AssertProductsAreEqual(IList<Acme.FulcrumFacade.Sl.WebApi.Model.Product> expected, IList<Acme.FulcrumFacade.Sl.WebApi.Model.Product> actual)
        {
            if (expected == null) throw new ArgumentNullException(nameof(expected));
            if (actual == null) throw new ArgumentNullException(nameof(actual));

            Assert.AreEqual(expected.Count, expected.Count, "Number of products");

            for (var i = 0; i < expected.Count; i++)
            {
                AssertProductsAreEqual(expected.ElementAt(i), actual.ElementAt(i));
            }
        }

        private void AssertProductsAreEqual(Acme.FulcrumFacade.Sl.WebApi.Model.Product expected, Acme.FulcrumFacade.Sl.WebApi.Model.Product actual)
        {
            Assert.AreEqual(expected.Id, actual.Id, "Product Id");
            Assert.AreEqual(expected.Name, actual.Name, "Product name");
            Assert.AreEqual(expected.Category, actual.Category, "Product Category");
            Assert.AreEqual(expected.Price, actual.Price, "Product Price");
            Assert.AreEqual(expected.DateAdded, actual.DateAdded, "Product DateAdded");
        }
        #endregion
    }
}
