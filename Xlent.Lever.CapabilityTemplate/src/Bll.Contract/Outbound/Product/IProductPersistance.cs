using Xlent.Lever.CapabilityTemplate.Bll.Contract.Inbound.Product;
using Xlent.Lever.Libraries2.Standard.Decoupling;
using Xlent.Lever.Libraries2.Standard.Decoupling.Model;
using Xlent.Lever.Libraries2.Standard.Storage.Model;

namespace Xlent.Lever.CapabilityTemplate.Bll.Contract.Outbound.Product
{
    /// <summary>
    /// CRUD logic for <seealso cref="IStorableProduct"/>.
    /// </summary>
    public interface IProductPersistance : ICrud<IStorableProduct, IConceptValue>, IFactory<IProduct>
    {
    }
}
