using System;
using Frobozz.PersonProfiles.FulcrumFacade.Contract.PersonProfiles;
using Xlent.Lever.Libraries2.Standard.Assert;
using Xlent.Lever.Libraries2.Standard.Storage.Model;

namespace Frobozz.PersonProfiles.FulcrumFacade.WebApi.Models
{
    /// <summary>
    /// A physical address.
    /// </summary>
    public class ServicePersonProfile : PersonProfile
    {
        internal static ServicePersonProfile ToImplementation(IPersonProfile item)
        {
            var implementation = item as ServicePersonProfile;
            InternalContract.RequireNotNull(implementation, nameof(item), $"Contract violation. Expected parameter {nameof(item)} to be of type {nameof(ServicePersonProfile)}, but was of type {item.GetType().Name}");
            return implementation;
        }
    }
}
