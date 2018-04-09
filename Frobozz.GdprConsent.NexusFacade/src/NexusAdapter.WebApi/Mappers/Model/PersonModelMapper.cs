using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Frobozz.CapabilityContracts.Gdpr.Model;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Contracts;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.MoveTo.Core.Mapping;

namespace Frobozz.GdprConsent.NexusAdapter.WebApi.Mappers.Model
{
    /// <inheritdoc />
    public class PersonModelMapper : IModelMapper<Person, IServerLogic, PersonTable>
    {
        /// <inheritdoc />
        public async Task<Person> CreateAndMapFromServerAsync(PersonTable source, IServerLogic serverLogic,
            CancellationToken token = default(CancellationToken))
        {
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var serverAddresses = await serverLogic.Address.ReadChildrenAsync(source.Id, token: token);
            var target = new Person
            {
                Id = MapperHelper.MapId<string, Guid>(source.Id),
                Name = source.Name,
                Etag = source.Etag,
                Addresses = serverAddresses.Select(CreateAndMapFromServer)
            };
            FulcrumAssert.IsValidated(target);
            return target;
        }

        /// <inheritdoc />
        public async Task<PersonTable> CreateAndMapToServerAsync(Person source, IServerLogic serverLogic, CancellationToken token = default(CancellationToken))
        {
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var id = MapperHelper.MapId<Guid, string>(source.Id);
            var target = new PersonTable
            {
                Id = id,
                Name = source.Name,
                Etag = source.Etag
            };
            FulcrumAssert.IsValidated(target);
            await UpdateAddressesAsync(id, source, serverLogic, token);
            return target;
        }

        private async Task UpdateAddressesAsync(Guid personId, Person person, IServerLogic serverLogic, CancellationToken token)
        {
            var serverAddresses = await serverLogic.Address.ReadChildrenAsync(personId, token: token);
            var serverAddressArray = serverAddresses as AddressTable[] ?? serverAddresses.ToArray();
            var tasks = new List<Task>();
            for (var typeInt = 1; typeInt < 5; typeInt++)
            {
                var typeString = ToAddressTypeClient(typeInt);
                var serverAddress = serverAddressArray.FirstOrDefault(a => a.Type == typeInt);
                var address = person.Addresses.FirstOrDefault(a => a.Type == typeString);
                if (serverAddress == null)
                {
                    if (address == null) continue;
                    serverAddress = ToServer(personId, address);
                    var task = serverLogic.Address.CreateAsync(serverAddress, token);
                    tasks.Add(task);
                }
                else
                {
                    Task task;
                    if (address == null)
                    {
                        task = serverLogic.Address.DeleteAsync(serverAddress.Id, token);
                    }
                    else
                    {
                        var updatedServerAddress = ToServer(personId, address);
                        updatedServerAddress.Id = serverAddress.Id;
                        updatedServerAddress.Etag = serverAddress.Etag;
                        task = serverLogic.Address.UpdateAsync(updatedServerAddress.Id, updatedServerAddress, token);
                    }
                    tasks.Add(task);
                }
            }
            await Task.WhenAll(tasks);
        }

        private AddressTable ToServer(Guid personId, Address source, bool nullIsOk = false)
        {
            if (nullIsOk && source == null) return null;
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var target = new AddressTable
            {
                Type = ToAddressTypeServer(source.Type),
                Street = source.Street,
                City = source.City,
                PersonId = personId
            };
            FulcrumAssert.IsValidated(target);
            return target;
        }

        private Address CreateAndMapFromServer(AddressTable source)
        {
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var target = new Address
            {
                Type = ToAddressTypeClient(source.Type),
                Street = source.Street,
                City = source.City
            };
            FulcrumAssert.IsValidated(target);
            return target;
        }

        private static int ToAddressTypeServer(string source)
        {
            switch (source)
            {
                case "Public": return 1;
                case "Invoice": return 2;
                case "Delivery": return 3;
                case "Postal": return 4;
                default:
                    FulcrumAssert.Fail($"Unknown address type ({source}).");
                    return 0;
            }
        }

        private static string ToAddressTypeClient(int source)
        {
            switch (source)
            {
                case 1: return "Public";
                case 2: return "Invoice";
                case 3:
                    return "Delivery";
                case 4: return "Postal";
                default:
                    FulcrumAssert.Fail($"Unknown address type ({source}).");
                    return "None";
            }
        }
    }
}