using Telerik.OpenAccess;
using WayneCarCorps.Data.Common;
using WayneCarCorps.MySQL.FluentModels;
using WayneCarCorps.MySQL.Models;

namespace WayneCarCorps.MySQL
{
    public class MySqlStartUp
    {
        public static void Main()
        {
            var context = new FluentModelContext();
            var repository = new OpenAccessRepository<SalesReport>(context);
            UpdateDatabase(context);
        }

        private static void UpdateDatabase(FluentModelContext context)
        {
            using (context)
            {
                var schemaHandler = context.GetSchemaHandler();
                EnsureDB(schemaHandler);
            }
        }

        private static void EnsureDB(ISchemaHandler schemaHandler)
        {
            string script = null;
            if (schemaHandler.DatabaseExists())
            {
                script = schemaHandler.CreateUpdateDDLScript(null);
            }
            else
            {
                schemaHandler.CreateDatabase();
                script = schemaHandler.CreateDDLScript();
            }

            if (!string.IsNullOrEmpty(script))
            {
                schemaHandler.ExecuteDDLScript(script);
            }
        }
    }
}