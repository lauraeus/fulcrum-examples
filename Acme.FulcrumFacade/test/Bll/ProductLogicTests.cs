using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Acme.FulcrumFacade.Bll;
using Acme.FulcrumFacade.Bll.Contract.Bll.Model;
using Acme.FulcrumFacade.Bll.Contract.Dal;

namespace Bll.Tests
{
    [TestClass]
    public class ProductLogicTests
    {
        private ProductLogic _productLogic;
        private Mock<IProductRepository> _productRepository;

        private Product _expectedProduct;

        [TestInitialize]
        public void Initialize()
        {
            _expectedProduct = GenerateProduct();
            _productRepository = new Mock<IProductRepository>();
            _productLogic = new ProductLogic(_productRepository.Object);
        }

        #region GetProduct()
        [TestMethod]
        public async Task GetProduct()
        {
            _productRepository.Setup(mock => mock.GetProduct(_expectedProduct.Id)).ReturnsAsync(_expectedProduct);
            var actualProduct = await _productLogic.GetProduct(_expectedProduct.Id.ToString());
            AssertProductsAreEqual(_expectedProduct, actualProduct);
        }
        #endregion

        #region GetAllProduct()
        [TestMethod]
        public async Task GetAllProducts()
        {
            var expectedProducts = GenerateProducts(3);
            _productRepository.Setup(mockRepo => mockRepo.GetAllProducts()).ReturnsAsync(expectedProducts);
            var actualProducts = await _productLogic.GetAllProducts();
            AssertProductsAreEqual(expectedProducts.ToList(), actualProducts.ToList());
        }
        #endregion

        #region UpdateProduct()
        [TestMethod]
        public async Task UpdateProduct()
        {
            _productRepository.Setup(mockRepo => mockRepo.UpdateProduct(It.IsNotNull<Product>())).ReturnsAsync(_expectedProduct);

            var actualProduct = await _productLogic.UpdateProduct(_expectedProduct);

            AssertProductsAreEqual(_expectedProduct, actualProduct);
        }
        #endregion

        #region CreateProduct
        [TestMethod]
        public async Task CreateProduct()
        {
            _productRepository.Setup(mockRepo => mockRepo.CreateProduct(It.IsNotNull<Product>())).ReturnsAsync(_expectedProduct);

            var actualProduct = await _productLogic.CreateProduct(_expectedProduct);

            AssertProductsAreEqual(_expectedProduct, actualProduct);
        }
        #endregion

        #region DeleteProduct
        [TestMethod]
        public async Task DeleteProduct()
        {
            _productRepository.Setup(mock => mock.DeleteProduct(_expectedProduct.Id)).ReturnsAsync(_expectedProduct);
            var actualProduct = await _productLogic.DeleteProduct(_expectedProduct.Id.ToString());
            AssertProductsAreEqual(_expectedProduct, actualProduct);
        }
        #endregion

        #region FactoryMethods
        private IList<Product> GenerateProducts(int numProducts)
        {
            var result = new List<Product>();
            for (int i = 1; i <= numProducts; i++)
            {
                result.Add(GenerateProduct(i));
            }

            return result;
        }

        private Product GenerateProduct(int id = 1)
        {
            return new Product
            {
                Id = id,
                Name = "sausage",
                Category = "food",
                DateAdded = new DateTimeOffset(new DateTime(2017, 1, 1), new TimeSpan(1, 0, 0))
            };
        }
        #endregion

        #region AssertionMethods

        private void AssertProductsAreEqual(IList<Product> expected, IList<Product> actual)
        {
            if (expected == null) throw new ArgumentNullException(nameof(expected));
            if (actual == null) throw new ArgumentNullException(nameof(actual));

            Assert.AreEqual(expected.Count, expected.Count, "Number of products");

            for (var i = 0; i < expected.Count; i++)
            {
                AssertProductsAreEqual(expected.ElementAt(i), actual.ElementAt(i));
            }
        }

        private void AssertProductsAreEqual(Product expected, Product actual)
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
