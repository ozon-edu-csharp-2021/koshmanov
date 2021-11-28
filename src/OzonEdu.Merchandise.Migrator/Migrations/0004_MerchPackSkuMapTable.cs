using FluentMigrator;

namespace OzonEdu.Merchandise.Migrator.Migrations
{
    [Migration(4)]
    public class MerchPackSkuMapTable:Migration {
        public override void Up()
        {
            Create.Table("merch_pack_sku_map")
                .WithColumn("merch_pack_id").AsInt64().PrimaryKey()
                .WithColumn("sku").AsInt64().PrimaryKey()
                .WithColumn("type_id").AsInt64().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("merch_pack_sku_map");
        }
    }
}