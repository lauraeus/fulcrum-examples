using System;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Contracts;
using Xlent.Lever.Libraries2.Core.Storage.Model;
using Xlent.Lever.Libraries2.SqlServer;
using Xlent.Lever.Libraries2.SqlServer.Model;

namespace Frobozz.GdprConsent.NexusAdapter.WebApi.Dal.SqlServer
{
    /// <inheritdoc />
    public class SqlServerStorage : IStorage
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
                EtagColumnName = "Etag",
            };
            var person = new SimpleTableHandler<PersonTable>(connectionString,tableMetadata);
            Person = person;

            // Consent
            tableMetadata = new SqlTableMetadata
            {
                TableName = "Consent",
                CustomColumnNames = new[] { "Name" },
                EtagColumnName = "Etag",
            };
            var consent = new SimpleTableHandler<ConsentTable>(connectionString, tableMetadata);
            Consent = consent;

            // Address
            tableMetadata = new SqlTableMetadata
            {
                TableName = "Address",
                CustomColumnNames = new[] { "Street", "City" },
                EtagColumnName = "Etag",
                ForeignKeyColumnName = "ParentId"
            };
            Address = new ManyToOneTableHandler<AddressTable,PersonTable>(connectionString, tableMetadata, "ParentId", person);

            // PersonContent
            tableMetadata = new SqlTableMetadata
            {
                TableName = "PersonContent",
                CustomColumnNames = new[] { "HasGivenConsent", "PersonId", "ConsentId" },
                EtagColumnName = "Etag"
            };
            PersonConsent =
                new ManyToOneTableHandler<PersonConsentTable, PersonTable>(connectionString, tableMetadata, "ParentId", person);
        }
    }
}