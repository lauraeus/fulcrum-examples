using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SupplierCompany.SystemFacade.Sl.WebApi.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SupplierCompany.SystemFacade.Sl.WebApi.Tests.Product
{
    [TestClass]
    public class ProductsControllerTests
    {
        private GeocodesController _controller;
        private Mock<IProductFunctionality> _productLogic;
        private Model.Product _slExpectedProduct;
        private IProduct _expectedProduct;

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
        }

        [TestInitialize]
        public void Initialize()
        {
           
            _productLogic = new Mock<IProductFunctionality>();
            _productLogic.Setup(mock => mock.CreateItemFromFactory()).Returns(new ProductMock());
            _controller = new GeocodesController(_productLogic.Object);

            _slExpectedProduct = ProductFactory.CreateProduct();
            _expectedProduct = ToBll(_slExpectedProduct);
        }

        private IProduct ToBll(Model.Product source)
        {
            return (IProduct) _controller.UnitTest_ToBll(source);
        }

        #region ReadAsync()
        [TestMethod]
        public async Task GetProduct()
        {
            _productLogic.Setup(mock => mock.ReadAsync(_expectedProduct.Id.ToString())).ReturnsAsync(_expectedProduct);
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
            _productLogic.Setup(mockRepo => mockRepo.ReadAllAsync()).ReturnsAsync(bllExpectedProducts);
            var actualProducts = await _controller.GetAllAsync();
            AssertProductsAreEqual(expectedProducts.ToList(), actualProducts.ToList());
        }
        #endregion

        #region Update()
        [TestMethod]
        public async Task UpdateProduct()
        {
            _productLogic.Setup(mockRepo => mockRepo.Update(It.IsNotNull<IProduct>())).ReturnsAsync(_expectedProduct);
            _slExpectedProduct.Validate("err loc");
            var actualProduct = await _controller.UpdateProduct(_slExpectedProduct);

            AssertProductsAreEqual(_slExpectedProduct, actualProduct);
        }
        #endregion

        #region CreateAsync()
        [TestMethod]
        public async Task CreateProduct()
        {
            _productLogic.Setup(mockRepo => mockRepo.CreateAsync(It.IsNotNull<IProduct>())).ReturnsAsync(_expectedProduct);
            _slExpectedProduct.Validate("err loc");
            var actualProduct = await _controller.CreateProduct(_slExpectedProduct);

            AssertProductsAreEqual(_slExpectedProduct, actualProduct);
        }
        #endregion

        #region Delete
        [TestMethod]
        public async Task DeleteProduct()
        {
            _productLogic.Setup(mock => mock.Delete(_expectedProduct.Id.ToString())).ReturnsAsync(_expectedProduct);
            var actualProduct = await _controller.DeleteProduct(_slExpectedProduct.Id);
            AssertProductsAreEqual(_slExpectedProduct, actualProduct);
        }
        #endregion

        #region AssertionMethods

        private void AssertProductsAreEqual(IList<Model.Product> expected, IList<Model.Product> actual)
        {
            if (expected == null) throw new ArgumentNullException(nameof(expected));
            if (actual == null) throw new ArgumentNullException(nameof(actual));

            Assert.AreEqual(expected.Count, expected.Count, "Number of products");

            for (var i = 0; i < expected.Count; i++)
            {
                AssertProductsAreEqual(expected.ElementAt(i), actual.ElementAt(i));
            }
        }

        private void AssertProductsAreEqual(Model.Product expected, Model.Product actual)
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
