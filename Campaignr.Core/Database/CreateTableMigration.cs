using Campaignr.Core.Database.Tables;
using Umbraco.Core.Migrations;

namespace Campaignr.Core.Database
{
    public class CreateTableMigration : MigrationBase
    {
        public CreateTableMigration(IMigrationContext context)
            : base(context)
        {
        }

        public override void Migrate()
        {
            if (!TableExists(SettingsTable.TableName))
            {
                Create.Table<SettingsTable>().Do();
            }
        }
    }
}
