using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Frobozz.CapabilityContracts.Gdpr.Model;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Contracts;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.MoveTo.Core.Crud.Mapping;

namespace Frobozz.GdprConsent.NexusAdapter.WebApi.Mappers.Model
{
    /// <inheritdoc />
    public class PersonModelMapper : IModelMapper<Person, IServerLogic, PersonTable>
    {
        /// <inheritdoc />
        public async Task<Person> MapFromServerAsync(PersonTable source, IServerLogic serverLogic,
            CancellationToken token = default(CancellationToken))
        {
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var serverAddresses = await serverLogic.Address.ReadChildrenAsync(source.Id, token: token);
            var target = new Person
            {
                Id = MapperHelper.MapToType<string, Guid>(source.Id),
                Name = source.Name,
                Etag = source.Etag,
                Addresses = serverAddresses.Select(CreateAndMapFromServer)
            };
            FulcrumAssert.IsValidated(target);
            return target;
        }

        /// <inheritdoc />
        public async Task<PersonTable> MapToServerAsync(Person source, IServerLogic serverLogic, CancellationToken token = default(CancellationToken))
        {
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var target = MapToServer(source);
            FulcrumAssert.IsValidated(target);
            FulcrumAssert.IsNotDefaultValue(target.Id);
            await UpdateAddressesAsync(target.Id, source, serverLogic, token);
            return target;
        }

        /// <inheritdoc />
        public PersonTable MapToServer(Person source)
        {
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var target = new PersonTable
            {
                Id = MapperHelper.MapToType<Guid, string>(source.Id),
                Name = source.Name,
                Etag = source.Etag
            };
            FulcrumAssert.IsValidated(target);
            return target;
        }

        private async Task UpdateAddressesAsync(Guid personId, Person person, IServerLogic serverLogic, CancellationToken token)
        {
            var serverAddresses = await serverLogic.Address.ReadChildrenAsync(personId, token: token);
            var serverAddressArray = serverAddresses as AddressTable[] ?? serverAddresses.ToArray();
            var tasks = new List<Task>();
            for (var typeInt = 1; typeInt < 5; typeInt++)
            {
                var typeString = MapperHelper.MapToType<string, int>(typeInt);
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
                Type = MapperHelper.MapToType<int, string>(source.Type),
                Street = source.Street,
                City = source.City,
                PersonId = personId
            };
            return target;
        }

        private Address CreateAndMapFromServer(AddressTable source)
        {
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var target = new Address
            {
                Type = MapperHelper.MapToType<string,int>(source.Type),
                Street = source.Street,
                City = source.City
            };
            FulcrumAssert.IsValidated(target);
            return target;
        }
    }
}