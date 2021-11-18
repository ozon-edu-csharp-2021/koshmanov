using  FluentMigrator;

namespace OzonEdu.Merchandise.Migrator.Migrations
{
    [Migration(1)]
    public class MerchOrderTable: Migration
    {
        public override void Up()
        {
            Create
                .Table("merch_order")
                .WithColumn("id").AsInt64().Identity().PrimaryKey()
                .WithColumn("employee_email").AsString().NotNullable()
                .WithColumn("merch_pack_id").AsInt32().NotNullable()
                .WithColumn("status_id").AsInt32().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("merch_order");
        }
    }
}