﻿using System.Data.Common;
using Microsoft.Data.Sqlite;

namespace chat_server.Persistence
{
    public sealed class SqliteCoreDbFactoryProvider : DbProviderFactory
    {
        public override bool CanCreateDataSourceEnumerator => false;

        public override DbCommand CreateCommand() =>
            SqliteFactory.Instance.CreateCommand();

        public override DbCommandBuilder CreateCommandBuilder() =>
            null;

        public override DbConnection CreateConnection() =>
            SqliteFactory.Instance.CreateConnection();

        public override DbConnectionStringBuilder CreateConnectionStringBuilder() =>
            SqliteFactory.Instance.CreateConnectionStringBuilder();

        public override DbDataAdapter CreateDataAdapter() =>
            new EmptyDataAdapter();

        public override DbDataSourceEnumerator CreateDataSourceEnumerator() => null;

        public override DbParameter CreateParameter() =>
            SqliteFactory.Instance.CreateParameter();
        
        private sealed class EmptyDataAdapter : DbDataAdapter{}
    }
}