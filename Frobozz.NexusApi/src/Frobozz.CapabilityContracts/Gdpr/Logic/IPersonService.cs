﻿using System.Threading;
using System.Threading.Tasks;
using Frobozz.CapabilityContracts.Gdpr.Model;
using Xlent.Lever.Libraries2.Core.Crud.Interfaces;

namespace Frobozz.CapabilityContracts.Gdpr.Logic
{
    public interface IPersonService : ICrud<Person, string>
    {
        /// <summary>
        /// Find the first person with an exact match for the name
        /// </summary>
        /// <param name="name">The name to have an exact match for.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns>The found person or null.</returns>
        Task<Person> FindFirstOrDefaultByNameAsync(string name, CancellationToken token = default(CancellationToken));
    }
}