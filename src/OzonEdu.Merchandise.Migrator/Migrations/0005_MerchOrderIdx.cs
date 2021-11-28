using FluentMigrator;

namespace OzonEdu.Merchandise.Migrator.Migrations
{
    [Migration(5, TransactionBehavior.None)]
    public class MerchOrderIdx:ForwardOnlyMigration {
        
        public override void Up()
        {
            Execute.Sql(@"
                CREATE INDEX CONCURRENTLY merch_order_id_idx ON merch_order (id)"
            );
        }
    }
}