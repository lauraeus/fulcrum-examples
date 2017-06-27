using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xlent.Lever.Libraries2.Standard.Error.Logic;

namespace Sl.WebApi.Tests.Product
{
    [TestClass]
    public class ProductModelValidationTests
    {
        private Acme.FulcrumFacade.Sl.WebApi.Model.Product _expectedProduct;

        [TestInitialize]
        public void Initialize()
        {
            _expectedProduct = ProductFactory.CreateProduct();
        }

        [TestMethod]
        [ExpectedException(typeof(FulcrumAssertionFailedException))]
        public void ProductNegativeId()
        {
            _expectedProduct.Id = "";
            _expectedProduct.Validate("err loc");
        }

        [TestMethod]
        [ExpectedException(typeof(FulcrumAssertionFailedException))]
        public void ProductNegativePrice()
        {
            _expectedProduct.Price = -1;
            _expectedProduct.Validate("err loc");
        }

        [TestMethod]
        [ExpectedException(typeof(FulcrumAssertionFailedException))]
        public void ProductNameWithNumbers()
        {
            _expectedProduct.Name = _expectedProduct.Name + -1;
            _expectedProduct.Validate("err loc");
        }

        [TestMethod]
        [ExpectedException(typeof(FulcrumAssertionFailedException))]
        public void ProductCategoryNameWithNumbers()
        {
            _expectedProduct.Category = _expectedProduct.Category + -1;
            _expectedProduct.Validate("err loc");
        }
    }
}
