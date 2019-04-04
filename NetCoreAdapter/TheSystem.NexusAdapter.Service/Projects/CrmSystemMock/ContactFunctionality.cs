using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheSystem.NexusAdapter.Service.Projects.CrmSystemContract;
using TheSystem.NexusAdapter.Service.Projects.CrmSystemContract.Model;

namespace TheSystem.NexusAdapter.Service.Projects.CrmSystemMock
{
    public class ContactFunctionality : IContactFunctionality
    {
        private static readonly List<Contact> Items = new List<Contact>();
        private int _memberCount;

        /// <inheritdoc />
        public Task<IEnumerable<Contact>> ReadAllAsync()
        {
            return Task.FromResult((IEnumerable<Contact>) Items);
        }

        internal Task<Guid> CreateAsync(Contact item)
        {
            item.Id = Guid.NewGuid();
            _memberCount++;
            item.CustomerNumber = $"Member {_memberCount}";
            Items.Add(item);
            return Task.FromResult(item.Id);
        }
     }
}