using System.Data.Entity;
using WayneCarCorps.Models;

namespace WayneCarCorps.Data
{
    public class WayneCarCorpsContext : DbContext
    {
        public WayneCarCorpsContext()
           : base("WayneCarCorpsDataBase")
        {
        }

        public virtual IDbSet<Address> Addresses { get; set; }

        public virtual IDbSet<Car> Cars { get; set; }

        public virtual IDbSet<CarType> CarTypes { get; set; }

        public virtual IDbSet<Color> Colors { get; set; }

        public virtual IDbSet<Country> Countries { get; set; }

        public virtual IDbSet<Dealer> Dealers { get; set; }

        public virtual IDbSet<Manufacturer> Manufacturers { get; set; }

        public virtual IDbSet<Model> Models { get; set; }

        public virtual IDbSet<Sale> Sales { get; set; }
    }
}
