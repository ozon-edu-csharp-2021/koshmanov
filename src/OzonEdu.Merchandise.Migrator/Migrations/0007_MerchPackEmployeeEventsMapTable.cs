using FluentMigrator;

namespace OzonEdu.Merchandise.Migrator.Migrations
{
    [Migration(4)]
    public class MerchPackEmployeeEventMapTable:Migration {
        public override void Up()
        {
            Create.Table("merch_pack_type_employee_event_map")
                .WithColumn("merch_pack_type_id").AsInt32().PrimaryKey()
                .WithColumn("employee_event").AsInt32().PrimaryKey();
        }

        public override void Down()
        {
            Delete.Table("merch_pack_type_employee_event_map");
        }
    }
}