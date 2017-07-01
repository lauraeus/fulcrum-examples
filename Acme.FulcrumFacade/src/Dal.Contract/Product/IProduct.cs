using System;
using Xlent.Lever.Libraries2.Standard.Assert;

namespace Acme.FulcrumFacade.Dal.Contract.Product
{
    public interface IProduct : IValidatable
    {
        string Category { get; set; }
        DateTimeOffset DateAdded { get; set; }
        int Id { get; set; }
        string Name { get; set; }
        double Price { get; set; }
    }
}