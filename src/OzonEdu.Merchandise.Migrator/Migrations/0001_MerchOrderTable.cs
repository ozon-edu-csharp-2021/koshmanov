using  FluentMigrator;

namespace OzonEdu.Merchandise.Migrator.Migrations
{
    [Migration(1)]
    public class MerchOrderTable: Migration
    {
        public override void Up()
        {
            Execute.Sql(@"CREATE TABLE if not exists merch_order(
            id BIGSERIAL PRIMARY KEY, 
            employee_id BIGINT NOT NULL,
            merch_pack_id BIGINT NOT NULL,
            status_id INT NOT NULL,
            order_date DATETIME NOT NULL)
            ");
        }

        public override void Down()
        {
            Delete.Table("merch_order");
        }
    }
}