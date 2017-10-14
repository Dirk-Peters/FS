using FluentMigrator;

namespace chat_server.Persistence.Migrations
{
    [Migration(1)]
    public sealed class InitialSchema : Migration
    {
        public override void Up()
        {
            var sessions = Create.Table("Sessions");
            sessions.WithColumn("Id").AsGuid().PrimaryKey();
            sessions.WithColumn("name").AsString();
        }

        public override void Down()
        {
            Delete.Table("Sessions");
        }
    }
}