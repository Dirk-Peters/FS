using System.Data.Common;
using FluentMigrator.Runner.Processors;

namespace chat_server.Persistence
{
    public sealed class SqliteCoreDbFactory : DbFactoryBase
    {
        protected override DbProviderFactory CreateFactory() => new SqliteCoreDbFactoryProvider();
    }
}