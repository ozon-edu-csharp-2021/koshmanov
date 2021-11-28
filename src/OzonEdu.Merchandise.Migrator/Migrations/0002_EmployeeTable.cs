using System;
using  FluentMigrator;

namespace OzonEdu.Merchandise.Migrator.Migrations
{
    [Migration(2)]
    public class EmployeeTable : Migration
    {
        public override void Up()
        {
            Create.Table("employee")
                .WithColumn("id").AsInt64().Identity().PrimaryKey()
                .WithColumn("email").AsString().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("employee");
        }
    }
}