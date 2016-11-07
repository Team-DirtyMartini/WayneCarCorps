using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WayneCarCorps.Models
{
    public class Address 
    {
        private IList<Dealer> dealers;
        private IList<Manufacturer> manufacturers;
        public Address()
        {
            this.Dealers = new List<Dealer>();
            this.Manufacturers = new List<Manufacturer>();
        }
        public int Id { get; set; }

        public string AddressLine { get; set; }

        public virtual int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual IList<Dealer> Dealers
        {
            get { return this.dealers; }
            set { this.dealers = value; }
        }

        public virtual IList<Manufacturer> Manufacturers
        {
            get { return this.manufacturers; }
            set { this.manufacturers = value; }
        }


    }
}
