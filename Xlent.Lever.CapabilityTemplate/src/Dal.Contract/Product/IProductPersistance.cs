using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xlent.Lever.Libraries2.Standard.Decoupling;
using Xlent.Lever.Libraries2.Standard.Storage.Model;

namespace Xlent.Lever.CapabilityTemplate.Dal.Contract.Product
{
    /// <summary>
    /// CRUD logic for <seealso cref="IStorableProduct"/>.
    /// </summary>
    public interface IProductPersistance : IDisposable, ICrud<IStorableProduct, int>, IFactory<IStorableProduct>
    {
    }
}
