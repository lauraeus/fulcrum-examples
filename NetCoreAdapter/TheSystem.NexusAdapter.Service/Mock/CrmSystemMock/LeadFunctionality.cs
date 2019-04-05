using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nexus.Link.Libraries.Core.Assert;
using Nexus.Link.Libraries.Core.Error.Logic;
using TheSystem.NexusAdapter.Service.Adapter.Contract;
using TheSystem.NexusAdapter.Service.System.CrmSystemContract;
using TheSystem.NexusAdapter.Service.System.CrmSystemContract.Model;

namespace TheSystem.NexusAdapter.Service.Mock.CrmSystemMock
{
    public class LeadFunctionality : ILeadFunctionality
    {
        private readonly ContactFunctionality _contactFunctionality;
        private readonly IAdapterService _adapterService;
        private static readonly List<Lead> Items = new List<Lead>();


        public LeadFunctionality(ContactFunctionality contactFunctionality, IAdapterService adapterService)
        {
            _contactFunctionality = contactFunctionality;
            _adapterService = adapterService;
        }
        /// <inheritdoc />
        public Task<IEnumerable<Lead>> ReadAllAsync()
        {
            return Task.FromResult((IEnumerable<Lead>)Items);
        }

        internal Task<Lead> ReadAsync(Guid id)
        {
            return Task.FromResult(Items.FirstOrDefault(i => i.Id == id));
        }

        /// <inheritdoc />
        public Task<Guid> CreateAsync(Lead item)
        {
            item.Id = Guid.NewGuid();
            item.Status = Lead.StatusEnum.Active;
            item.UpdatedAt = DateTimeOffset.Now;
            Items.Add(item);
            return Task.FromResult(item.Id);
        }

        /// <inheritdoc />
        public async Task<Guid> QualifyAsync(Guid id)
        {
            var lead = await ReadAsync(id);
            // TODO: Must not use Fulcrum stuff in the CRM system
            if (lead == null) throw new FulcrumNotFoundException("Lead not found");
            switch (lead.Status)
            {
                case Lead.StatusEnum.Active:
                    lead.Status = Lead.StatusEnum.Qualified;
                    lead.Reason = "Qualified";
                    lead.UpdatedAt = DateTimeOffset.Now;
                    var contact = new Contact
                    {
                        Name = lead.Name,
                        OriginatingLeadId = lead.Id
                    };
                    var contactId = await _contactFunctionality.CreateAsync(contact);
                    await _adapterService.LeadWasQualified(lead.Id, contactId, lead.UpdatedAt);
                    return contactId;
                case Lead.StatusEnum.Qualified:
                    // TODO: Must not use Fulcrum stuff in the CRM system
                    throw new FulcrumBusinessRuleException("The lead was already qualified.");
                case Lead.StatusEnum.Rejected:
                    throw new FulcrumBusinessRuleException("The lead has previously been rejected.");
                default:
                    // TODO: Must not use Fulcrum stuff in the CRM system
                    FulcrumAssert.Fail($"Unknown lead status: {lead.Status}");
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <inheritdoc />
        public async Task RejectAsync(Guid id, string reason)
        {
            var lead = await ReadAsync(id);
            // TODO: Must not use Fulcrum stuff in the CRM system
            if (lead == null) throw new FulcrumNotFoundException("Lead not found");
            switch (lead.Status)
            {
                case Lead.StatusEnum.Active:
                    lead.Status = Lead.StatusEnum.Rejected;
                    lead.Reason = reason;
                    lead.UpdatedAt = DateTimeOffset.Now;
                    return;
                case Lead.StatusEnum.Qualified:
                    // TODO: Must not use Fulcrum stuff in the CRM system
                    throw new FulcrumBusinessRuleException("The lead has already been qualified and can't be rejected.");
                case Lead.StatusEnum.Rejected:
                    // Idempotent - already rejected
                    return;
                default:
                    // TODO: Must not use Fulcrum stuff in the CRM system
                    FulcrumAssert.Fail($"Unknown lead status: {lead.Status}");
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}