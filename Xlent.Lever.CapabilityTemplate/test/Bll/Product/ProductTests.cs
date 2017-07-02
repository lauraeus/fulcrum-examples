using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xlent.Lever.CapabilityTemplate.Bll.Contract.Inbound.Product;
using Xlent.Lever.CapabilityTemplate.Bll.Product;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using DM = Xlent.Lever.CapabilityTemplate.Dal.Contract.Product;

namespace Xlent.Lever.CapabilityTemplate.Bll.Tests.Product
{
    [TestClass]
    public class ProductTests
    {
        private IProductFunctionality _productFunctionality;
        private Mock<DM.IProductPersistance> _productRepository;

        private IProduct _expectedProduct;

        [TestInitialize]
        public void Initialize()
        {
            _expectedProduct = GenerateProduct();
            _productRepository = new Mock<DM.IProductPersistance>();
            _productRepository.Setup(mock => mock.ProductFactory()).Returns(new StorableProductMock());
            _productFunctionality = new ProductFunctionality(_productRepository.Object);
        }

        #region ReadAsync()
        [TestMethod]
        public async Task GetProduct()
        {
            _productRepository.Setup(mock => mock.ReadAsync(_expectedProduct.Id)).ReturnsAsync(ToDal(_expectedProduct));

            var actualProduct = await _productFunctionality.ReadAsync(_expectedProduct.Id.ToString());
            AssertProductsAreEqual(_expectedProduct, actualProduct);
        }

        #endregion

        #region GetAllProduct()
        [TestMethod]
        public async Task GetAllProducts()
        {
            var expectedProducts = GenerateProducts(3);
            _productRepository.Setup(mockRepo => mockRepo.ReadAllAsync()).ReturnsAsync(expectedProducts.Select(ToDal));
            var actualProducts = await _productFunctionality.ReadAllAsync();
            AssertProductsAreEqual(expectedProducts.ToList(), actualProducts.ToList());
        }
        #endregion

        #region UpdateAsync()
        [TestMethod]
        public async Task UpdateProduct()
        {
            _productRepository.Setup(mockRepo => mockRepo.UpdateAsync(It.IsNotNull<DM.IStorableProduct>())).ReturnsAsync(ToDal(_expectedProduct));

            var actualProduct = await _productFunctionality.Update(_expectedProduct);

            AssertProductsAreEqual(_expectedProduct, actualProduct);
        }
        #endregion

        #region CreateAsync
        [TestMethod]
        public async Task CreateProduct()
        {
            _productRepository.Setup(mockRepo => mockRepo.CreateAsync(It.IsNotNull<DM.IStorableProduct>())).ReturnsAsync(ToDal(_expectedProduct));

            var actualProduct = await _productFunctionality.CreateAsync(_expectedProduct);

            AssertProductsAreEqual(_expectedProduct, actualProduct);
        }
        #endregion

        #region DeleteAsync
        [TestMethod]
        public async Task DeleteProduct()
        {
            _productRepository.Setup(mock => mock.DeleteAsync(_expectedProduct.Id)).ReturnsAsync(ToDal(_expectedProduct));
            var actualProduct = await _productFunctionality.Delete(_expectedProduct.Id.ToString());
            AssertProductsAreEqual(_expectedProduct, actualProduct);
        }
        #endregion

        #region Mapping
        private DM.IStorableProduct ToDal(IProduct source)
        {
            return (DM.IStorableProduct)_productFunctionality.UnitTest_ToDal(source);
        }
        #endregion

        #region FactoryMethods
        private IList<IProduct> GenerateProducts(int numProducts)
        {
            var result = new List<IProduct>();
            for (int i = 1; i <= numProducts; i++)
            {
                result.Add(GenerateProduct(i));
            }

            return result;
        }

        private IProduct GenerateProduct(int id = 1)
        {
            return new Bll.Product.ProductModel()
            {
                Id = id,
                Name = "sausage",
                Category = "food",
                DateAdded = new DateTimeOffset(new DateTime(2017, 1, 1), new TimeSpan(1, 0, 0))
            };
        }
        #endregion

        #region AssertionMethods

        private void AssertProductsAreEqual(IList<IProduct> expected, IList<IProduct> actual)
        {
            if (expected == null) throw new ArgumentNullException(nameof(expected));
            if (actual == null) throw new ArgumentNullException(nameof(actual));

            Assert.AreEqual(expected.Count, expected.Count, "Number of products");

            for (var i = 0; i < expected.Count; i++)
            {
                AssertProductsAreEqual(expected.ElementAt(i), actual.ElementAt(i));
            }
        }

        private void AssertProductsAreEqual(IProduct expected, IProduct actual)
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
