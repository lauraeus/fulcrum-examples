using System.Collections.Generic;
using System.Threading.Tasks;
using Acme.FulcrumFacade.Bll.Contract.Bll.Model;

namespace Acme.FulcrumFacade.Bll.Contract.Bll.Interface
{
    public interface IProductLogic
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProduct(string id);
        Task<Product> CreateProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task<Product> DeleteProduct(string id);
    }
}
