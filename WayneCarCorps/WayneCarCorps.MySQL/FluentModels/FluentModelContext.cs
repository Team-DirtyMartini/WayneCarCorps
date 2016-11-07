using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using WayneCarCorps.Models;
using WayneCarCorps.MySQL.Models;

namespace WayneCarCorps.MySQL.FluentModels
{
    public class FluentModelContext : OpenAccessContext
    {
        private const string connectionStringName = "WayneCarCorpsMySQL";

        private static BackendConfiguration backend = GetBackendConfiguration();

        private static MetadataSource metadataSource = new FluentModelMetadataSource();

        public FluentModelContext()
            : base(connectionStringName, backend, metadataSource)
        { }

        public IQueryable<SalesReport> Sales
        {
            get
            {
                return this.GetAll<SalesReport>();
            }
        }

        public static BackendConfiguration GetBackendConfiguration()
        {
            BackendConfiguration backend = new BackendConfiguration();
            backend.Backend = "MySql";
            backend.ProviderName = "MySql.Data.MySqlClient";

            return backend;
        }
    }
}