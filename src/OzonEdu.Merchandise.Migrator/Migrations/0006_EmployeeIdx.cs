using FluentMigrator;

namespace OzonEdu.Merchandise.Migrator.Migrations
{
    [Migration(5, TransactionBehavior.None)]
    public class EmployeeIdx:ForwardOnlyMigration {
        
        public override void Up()
        {
            Execute.Sql(@"
                CREATE INDEX CONCURRENTLY employee_id_idx ON employee (id)"
            );
        }
    }
}