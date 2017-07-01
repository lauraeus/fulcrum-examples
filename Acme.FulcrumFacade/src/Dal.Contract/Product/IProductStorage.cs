using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acme.FulcrumFacade.Dal.Contract.Product
{
    public interface IProductStorage : IDisposable
    {
        Task<IEnumerable<IProduct>> GetAllProducts();
        Task<IProduct> GetProduct(int id);
        Task<IProduct> CreateProduct(IProduct product);
        Task<IProduct> UpdateProduct(IProduct product);
        Task<IProduct> DeleteProduct(int id);
        IProduct ProductFactory();
    }
}
