﻿using System;
using Frobozz.GdprConsent.Logic.Dal.Contracts;
using Nexus.Link.Libraries.Crud.Interfaces;
using Nexus.Link.Libraries.SqlServer;
using Nexus.Link.Libraries.SqlServer.Model;

namespace Frobozz.GdprConsent.Logic.Dal.SqlServer
{
    /// <inheritdoc />
    public class SqlServerStorage : IStorage
    {
        /// <inheritdoc />
        public ICrud<PersonTable, Guid> Person { get; }

        /// <inheritdoc />
        public ICrud<ConsentTable, Guid> Consent { get; }

        /// <inheritdoc />
        public ICrudManyToOne<AddressTable, Guid> Address { get; }

        /// <inheritdoc />
        public ICrudManyToMany<PersonConsentTable, PersonTable, ConsentTable, Guid> PersonConsent { get; }

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
                new ManyToManySql<PersonConsentTable, PersonTable, ConsentTable>(connectionString, tableMetadata, "PersonId", person, "ConsentId", consent);
        }
    }
}