using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acme.FulcrumFacade.Bll.Product;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using DM = Acme.FulcrumFacade.Dal.Contract.Product;
using BM = Acme.FulcrumFacade.Bll.Contract.Product;

namespace Acme.FulcrumFacade.Bll.Tests.Product
{
    [TestClass]
    public class ProductTests
    {
        private BM.IProductLogic _productLogic;
        private Mock<DM.IProductStorage> _productRepository;

        private BM.IProduct _expectedProduct;

        [TestInitialize]
        public void Initialize()
        {
            _expectedProduct = GenerateProduct();
            _productRepository = new Mock<DM.IProductStorage>();
            _productRepository.Setup(mock => mock.ProductFactory()).Returns(new ProductMock());
            _productLogic = new ProductLogic(_productRepository.Object);
        }

        #region GetProduct()
        [TestMethod]
        public async Task GetProduct()
        {
            _productRepository.Setup(mock => mock.GetProduct(_expectedProduct.Id)).ReturnsAsync(ToDal(_expectedProduct));

            var actualProduct = await _productLogic.GetProduct(_expectedProduct.Id.ToString());
            AssertProductsAreEqual(_expectedProduct, actualProduct);
        }

        #endregion

        #region GetAllProduct()
        [TestMethod]
        public async Task GetAllProducts()
        {
            var expectedProducts = GenerateProducts(3);
            _productRepository.Setup(mockRepo => mockRepo.GetAllProducts()).ReturnsAsync(expectedProducts.Select(ToDal));
            var actualProducts = await _productLogic.GetAllProducts();
            AssertProductsAreEqual(expectedProducts.ToList(), actualProducts.ToList());
        }
        #endregion

        #region UpdateProduct()
        [TestMethod]
        public async Task UpdateProduct()
        {
            _productRepository.Setup(mockRepo => mockRepo.UpdateProduct(It.IsNotNull<DM.IProduct>())).ReturnsAsync(ToDal(_expectedProduct));

            var actualProduct = await _productLogic.UpdateProduct(_expectedProduct);

            AssertProductsAreEqual(_expectedProduct, actualProduct);
        }
        #endregion

        #region CreateProduct
        [TestMethod]
        public async Task CreateProduct()
        {
            _productRepository.Setup(mockRepo => mockRepo.CreateProduct(It.IsNotNull<DM.IProduct>())).ReturnsAsync(ToDal(_expectedProduct));

            var actualProduct = await _productLogic.CreateProduct(_expectedProduct);

            AssertProductsAreEqual(_expectedProduct, actualProduct);
        }
        #endregion

        #region DeleteProduct
        [TestMethod]
        public async Task DeleteProduct()
        {
            _productRepository.Setup(mock => mock.DeleteProduct(_expectedProduct.Id)).ReturnsAsync(ToDal(_expectedProduct));
            var actualProduct = await _productLogic.DeleteProduct(_expectedProduct.Id.ToString());
            AssertProductsAreEqual(_expectedProduct, actualProduct);
        }
        #endregion

        #region Mapping
        private DM.IProduct ToDal(BM.IProduct source)
        {
            return (DM.IProduct)_productLogic.UnitTest_ToDal(source);
        }
        #endregion

        #region FactoryMethods
        private IList<BM.IProduct> GenerateProducts(int numProducts)
        {
            var result = new List<BM.IProduct>();
            for (int i = 1; i <= numProducts; i++)
            {
                result.Add(GenerateProduct(i));
            }

            return result;
        }

        private BM.IProduct GenerateProduct(int id = 1)
        {
            return new Bll.Product.Product()
            {
                Id = id,
                Name = "sausage",
                Category = "food",
                DateAdded = new DateTimeOffset(new DateTime(2017, 1, 1), new TimeSpan(1, 0, 0))
            };
        }
        #endregion

        #region AssertionMethods

        private void AssertProductsAreEqual(IList<BM.IProduct> expected, IList<BM.IProduct> actual)
        {
            if (expected == null) throw new ArgumentNullException(nameof(expected));
            if (actual == null) throw new ArgumentNullException(nameof(actual));

            Assert.AreEqual(expected.Count, expected.Count, "Number of products");

            for (var i = 0; i < expected.Count; i++)
            {
                AssertProductsAreEqual(expected.ElementAt(i), actual.ElementAt(i));
            }
        }

        private void AssertProductsAreEqual(BM.IProduct expected, BM.IProduct actual)
        {
            Assert.AreEqual(expected.Id, actual.Id, "IProduct Id");
            Assert.AreEqual(expected.Name, actual.Name, "IProduct name");
            Assert.AreEqual(expected.Category, actual.Category, "IProduct Category");
            Assert.AreEqual(expected.Price, actual.Price, "IProduct Price");
            Assert.AreEqual(expected.DateAdded, actual.DateAdded, "IProduct DateAdded");
        }
        #endregion
    }
}
