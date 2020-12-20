using NPoco;
using NPoco.Expressions;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Umbraco.Core.Composing;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace Campaignr.Core.Database.Tables
{
    public static class SettingsTableStore
    {
        public static SettingsTable GetSetting(string name)
        {
            SettingsTable result = null;

            if (!string.IsNullOrWhiteSpace(name))
            {
                using (var scope = Current.ScopeProvider.CreateScope())
                {
                    var sql = scope.SqlContext.Sql()
                        .Select<SettingsTable>()
                        .From<SettingsTable>()
                        .Where<SettingsTable>(x => x.Name.ToLower() == name.ToLower());

                    result = scope.Database.Fetch<SettingsTable>(sql).FirstOrDefault();

                    scope.Complete();
                }
            }

            return result;
        }

        public static string GetSettingValue(string name)
        {
            SettingsTable result = GetSetting(name);

            if (result != null)
            {
                return result.Value;
            }

            return "";
        }

        public static void SaveSetting(string name, string value)
        {
            SettingsTable setting = GetSetting(name);

            using (var scope = Current.ScopeProvider.CreateScope())
            {
                if (setting != null)
                {
                    setting.Value = value;
                    scope.Database.Update(setting);
                }
                else
                {
                    setting = new SettingsTable
                    {
                        Name = name,
                        Value = value
                    };
                    scope.Database.Save(setting);
                }

                scope.Complete();
            }
        }
    }

    [TableName(TableName)]
    [ExplicitColumns]
    [PrimaryKey("Id", AutoIncrement = true)]
    public class SettingsTable
    {
        public const string TableName = "Campaignr_Settings";

        [Column("Id")]
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Value")]
        public string Value { get; set; }

    }
}