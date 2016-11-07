using System.Collections.Generic;

namespace WayneCarCorps.Models
{
    public class Address 
    {
        private ICollection<Dealer> dealers;
        private ICollection<Manufacturer> manufacturers;
        public Address()
        {
            this.Dealers = new HashSet<Dealer>();
            this.Manufacturers = new HashSet<Manufacturer>();
        }
        public int Id { get; set; }

        public string AddressLine { get; set; }

        public virtual int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<Dealer> Dealers
        {
            get { return this.dealers; }
            set { this.dealers = value; }
        }

        public virtual ICollection<Manufacturer> Manufacturers
        {
            get { return this.manufacturers; }
            set { this.manufacturers = value; }
        }
    }
}
