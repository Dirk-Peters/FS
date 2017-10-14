using chat_server.Persistence;
using chat_server.Persistence.Migrations;
using FluentMigrator;
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
        [Test]
        public static void Can_execute_fluent_migrator_with_current_migrations()
        {
            using (var dbFile = new TempFile("sqlite"))
            {
                var connection = new SqliteConnection($"Data Source={dbFile.Path};");
                IRunnerContext context = new RunnerContext(new ConsoleAnnouncer());
                IMigrationProcessor processor = new SQLiteProcessor(
                    connection,
                    new SQLiteGenerator(),
                    new ConsoleAnnouncer(),
                    new ProcessorOptions(),
                    new SqliteCoreDbFactory());
                var runner = new MigrationRunner(typeof(InitialSchema).Assembly, context, processor);
                Assert.DoesNotThrow(() => runner.MigrateUp());
            }
        }
    }
}