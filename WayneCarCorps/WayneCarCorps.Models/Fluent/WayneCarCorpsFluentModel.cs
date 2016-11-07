using System.Linq;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;

namespace WayneCarCorps.Models.Fluent
{
    public partial class WayneCarCorpsFluentModel : OpenAccessContext
    {
        private const string connectionStringName = "WayneCarCorpsMySQL";

        private static BackendConfiguration backend = GetBackendConfiguration();

        private static MetadataSource metadataSource = new WayneCarCorpsFluentMetadata();

        public WayneCarCorpsFluentModel()
            :base(connectionStringName, backend, metadataSource)
        { }

        public IQueryable<Car> Cars
        {
            get
            {
                return this.GetAll<Car>();
            }
        }

        public IQueryable<CarType> CarTypes
        {
            get
            {
                return this.GetAll<CarType>();
            }
        }

        public IQueryable<Manufacturer> Manufacturers
        {
            get
            {
                return this.GetAll<Manufacturer>();
            }
        }

        public IQueryable<Model> Models
        {
            get
            {
                return this.GetAll<Model>();
            }
        }

        public IQueryable<Color> Colors
        {
            get
            {
                return this.GetAll<Color>();
            }
        }

        public IQueryable<Dealer> Dealers
        {
            get
            {
                return this.GetAll<Dealer>();
            }
        }

        public IQueryable<Address> Adresses
        {
            get
            {
                return this.GetAll<Address>();
            }
        }

        public IQueryable<Country> Countries
        {
            get
            {
                return this.GetAll<Country>();
            }
        }

        public IQueryable<Sale> Sales
        {
            get
            {
                return this.GetAll<Sale>();
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
