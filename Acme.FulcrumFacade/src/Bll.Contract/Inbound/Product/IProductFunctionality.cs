using Xlent.Lever.Libraries2.Standard.Decoupling;
using Xlent.Lever.Libraries2.Standard.Decoupling.Model;
using Xlent.Lever.Libraries2.Standard.Storage.Model;

namespace Acme.FulcrumFacade.Bll.Contract.Inbound.Product
{
    /// <summary>
    /// Product logic
    /// </summary>
    public interface IProductFunctionality : ICrud<IProduct, IConceptValue>, IFactory<IProduct>
    {
        /// <summary>
        /// Made for unit tests, so they can convert an <see cref="IProduct"/> to a IStorableProduct
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        object UnitTest_ToDal(IProduct source);
    }
}
