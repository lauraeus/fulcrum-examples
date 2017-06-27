using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Rest;
using Xlent.Lever.Libraries2.Standard.Assert;
using Xlent.Lever.Libraries2.Standard.Error.Logic;
using Acme.FulcrumFacade.Bll.Contract.Bll.Model;
using Acme.FulcrumFacade.Bll.Contract.Dal;

namespace Acme.FulcrumFacade.Dal.WebApi
{
    public class MockProductRepository : IProductRepository
    {
        private static readonly string Namespace = typeof(MockProductRepository).Namespace;
        private Product[] _products = {
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1, DateAdded = new DateTime(2016,1,13)},
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75, DateAdded = new DateTime(2015,12,1)},
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99,  DateAdded = new DateTime(2017,1,17)}
          };

        public Task<IEnumerable<Product>> GetAllProducts()
        {
            return Task.FromResult<IEnumerable<Product>>(_products);
        }

        public Task<Product> GetProduct(int id)
        {
            InternalContract.RequireGreaterThan(0, id, nameof(id));
            try
            {
                var product = GetProductOrThrowHttpNotFound(id);
                return Task.FromResult(product);
            }
            catch (HttpOperationException exception) when (exception.Response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new FulcrumNotFoundException(exception.Response.Content);
            }
        }

        public Task<Product> CreateProduct(Product product)
        {
            InternalContract.RequireValidatedAndNotNull(product, nameof(product));
            try
            {
                ThrowHttpConflictIfProductWithIdExists(product.Id);
                return Task.FromResult(product);
            }
            catch (HttpOperationException exception) when (exception.Response.StatusCode == HttpStatusCode.Conflict)
            {
                throw new FulcrumConflictException(exception.Response.Content);
            }
        }

        public Task<Product> UpdateProduct(Product product)
        {
            InternalContract.RequireValidatedAndNotNull(product, nameof(product));
            try
            {
                GetProductOrThrowHttpNotFound(product.Id);
            }
            catch (HttpOperationException exception) when (exception.Response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new FulcrumNotFoundException(exception.Response.Content);
            }
            return Task.FromResult(product);
        }

        public Task<Product> DeleteProduct(int id)
        {
            InternalContract.RequireGreaterThan(0, id, nameof(id));
            try
            {
                return Task.FromResult(GetProductOrThrowHttpNotFound(id));
            }
            catch (HttpOperationException exception) when (exception.Response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new FulcrumNotFoundException(exception.Response.Content);
            }
        }

        public void Dispose()
        {
            _products = null;
        }

        #region HelperMethods
        private Product GetProductOrThrowHttpNotFound(int productId)
        {
            var product = _products.FirstOrDefault(p => p.Id == productId);
            if (product == null) throw new HttpOperationException
            {
                Response = new HttpResponseMessageWrapper(new HttpResponseMessage(HttpStatusCode.NotFound), $"Product with Id: {productId} not found")
            };
            FulcrumAssert.IsValidatedAndNotNull(product, $"{Namespace}: 923656B6-3C2F-4D3C-85AC-A65D5A04D405");
            return product;
        }

        private void ThrowHttpConflictIfProductWithIdExists(int productId)
        {
            var product = _products.FirstOrDefault(p => p.Id == productId);

            if (product != null) throw new HttpOperationException
            {
                Response = new HttpResponseMessageWrapper(new HttpResponseMessage(HttpStatusCode.Conflict), $"Product with Id: {productId} already exists")
            };
        }
        #endregion
    }
}