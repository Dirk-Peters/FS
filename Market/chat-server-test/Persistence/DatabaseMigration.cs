using System;
using System.Data;
using chat_server.Persistence;
using chat_server.Persistence.Migrations;
using Dapper;
using FluentAssertions;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Generators.SQLite;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors;
using FluentMigrator.Runner.Processors.SQLite;
using Microsoft.Data.Sqlite;
using NUnit.Framework;

namespace chat_server_test.Persistence
{
    [TestFixture]
    public static class DatabaseMigration
    {
        private static MigrationRunner MigrationRunner(IDbConnection connection) =>
            new MigrationRunner(
                typeof(InitialSchema).Assembly,
                new RunnerContext(new ConsoleAnnouncer()),
                new SQLiteProcessor(
                    connection,
                    new SQLiteGenerator(),
                    new ConsoleAnnouncer(),
                    new ProcessorOptions(),
                    new SqliteCoreDbFactory()));

        private static SqliteConnection SqLiteFileConnection(TempFile dbFile) =>
            new SqliteConnection($"Data Source={dbFile.Path};");

        [Test]
        public static void Can_execute_fluent_migrator_with_current_migrations()
        {
            using (var dbFile = new TempFile("sqlite"))
            using (var connection = SqLiteFileConnection(dbFile))
            {
                Assert.DoesNotThrow(() => MigrationRunner(connection).MigrateUp());
            }
        }

        [Test]
        public static void Can_insert_rows_with_dapper()
        {
            using (var dbFile = new TempFile("sqlite"))
            using (var connection = SqLiteFileConnection(dbFile))
            {
                MigrationRunner(connection).MigrateUp();
                connection.Execute(@"insert into Sessions(Id,Name) values (@Id,@Name)",
                        new {Id = Guid.NewGuid(), Name = "test"})
                    .Should().Be(1);
            }
        }
    }
}