using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acme.FulcrumFacade.Bll.Contract.Product
{
    public interface IProductLogic
    {
        Task<IEnumerable<IProduct>> GetAllProducts();
        Task<IProduct> GetProduct(string id);
        Task<IProduct> CreateProduct(IProduct product);
        Task<IProduct> UpdateProduct(IProduct product);
        Task<IProduct> DeleteProduct(string id);
        IProduct ProductFactory();
        object UnitTest_ToDal(IProduct source);
    }
}
