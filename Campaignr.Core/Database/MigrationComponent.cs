using System;
using Umbraco.Core.Composing;
using Umbraco.Core.Logging;
using Umbraco.Core.Migrations;
using Umbraco.Core.Migrations.Upgrade;
using Umbraco.Core.Scoping;
using Umbraco.Core.Services;

namespace Campaignr.Core.Database
{
    [RuntimeLevel(MinLevel = Umbraco.Core.RuntimeLevel.Run)]
    public class MigrationComponentComposer : ComponentComposer<MigrationComponent>
    { }

    public class MigrationComponent : IComponent
    {
        private readonly IScopeProvider scopeProvider;
        private readonly IMigrationBuilder migrationBuilder;
        private readonly IKeyValueService keyValueService;
        private readonly IProfilingLogger logger;

        public MigrationComponent(
          IScopeProvider scopeProvider,
          IMigrationBuilder migrationBuilder,
          IKeyValueService keyValueService,
          IProfilingLogger logger)
        {
            this.scopeProvider = scopeProvider;
            this.migrationBuilder = migrationBuilder;
            this.keyValueService = keyValueService;
            this.logger = logger;
        }

        public void Initialize()
        {
            // Create a migration plan for a specific project/feature
            // We can then track that latest migration state/step for this project/feature
            var migrationPlan = new MigrationPlan("Campaignr");

            // This is the steps we need to take
            // Each step in the migration adds a unique value
            migrationPlan.From(string.Empty)
                .To<CreateTableMigration>("settings-db");

            // register and run our migration plan 
            var upgrader = new Upgrader(migrationPlan);
            upgrader.Execute(scopeProvider, migrationBuilder, keyValueService, logger);
        }

        public void Terminate()
        {
            // umbraco closing down
        }
    }
}
