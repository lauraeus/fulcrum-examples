using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheSystem.NexusAdapter.Service.CrmSystemContract;
using TheSystem.NexusAdapter.Service.CrmSystemContract.Model;

namespace TheSystem.NexusAdapter.Service.CrmSystemMock
{
    public class ContactFunctionality : IContactFunctionality
    {
        private static readonly List<Contact> _items = new List<Contact>();
        private int _memberCount;

        /// <inheritdoc />
        public Task<IEnumerable<Contact>> ReadAllAsync()
        {
            return Task.FromResult((IEnumerable<Contact>) _items);
        }

        internal Task<Guid> CreateAsync(Contact item)
        {
            item.Id = Guid.NewGuid();
            _memberCount++;
            item.CustomerNumber = $"Member {_memberCount}";
            _items.Add(item);
            return Task.FromResult(item.Id);
        }
     }
}