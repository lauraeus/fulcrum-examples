using System;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Contracts;
using Xlent.Lever.Libraries2.Core.Storage.Model;
using Xlent.Lever.Libraries2.SqlServer;
using Xlent.Lever.Libraries2.SqlServer.Model;

namespace Frobozz.GdprConsent.NexusAdapter.WebApi.Dal.SqlServer
{
    /// <inheritdoc />
    public class SqlServerStorage : IServerLogic
    {
        /// <inheritdoc />
        public ICrud<PersonTable, Guid> Person { get; }

        /// <inheritdoc />
        public ICrud<ConsentTable, Guid> Consent { get; }

        /// <inheritdoc />
        public IManyToOneRelationComplete<AddressTable, Guid> Address { get; }

        /// <inheritdoc />
        public IManyToOneRelationComplete<PersonConsentTable, Guid> PersonConsent { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        public SqlServerStorage(string connectionString)
        {
            // Person
            var tableMetadata = new SqlTableMetadata
            {
                TableName = "Person",
                CustomColumnNames = new [] {"Name"},
                OrderBy = new []{"Name"},
                EtagColumnName = "Etag",
            };
            var person = new CrudSql<PersonTable>(connectionString,tableMetadata);
            Person = person;

            // Consent
            tableMetadata = new SqlTableMetadata
            {
                TableName = "Consent",
                CustomColumnNames = new[] { "Name" },
                OrderBy = new[] { "Name" },
                EtagColumnName = "Etag",
            };
            var consent = new CrudSql<ConsentTable>(connectionString, tableMetadata);
            Consent = consent;

            // Address
            tableMetadata = new SqlTableMetadata
            {
                TableName = "Address",
                CustomColumnNames = new[] { "Type", "Street", "City", "PersonId" },
                OrderBy = new[] { "Type" },
                EtagColumnName = "Etag",
                ForeignKeyColumnName = "PersonId"
            };
            Address = new ManyToOneSql<AddressTable,PersonTable>(connectionString, tableMetadata, "PersonId", person);

            // PersonConsent
            tableMetadata = new SqlTableMetadata
            {
                TableName = "PersonConsent",
                CustomColumnNames = new[] { "HasGivenConsent", "PersonId", "ConsentId" },
                OrderBy = new string[] { },
                EtagColumnName = "Etag",
                ForeignKeyColumnName = "PersonId"
            };
            PersonConsent =
                new ManyToOneSql<PersonConsentTable, PersonTable>(connectionString, tableMetadata, "PersonId", person);
        }
    }
}