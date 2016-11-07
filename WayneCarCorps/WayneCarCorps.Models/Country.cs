using System.Collections.Generic;

namespace WayneCarCorps.Models
{
    public class Country
    {
        private ICollection<Address> addresses;
        public Country()
        {
            this.Addresses = new HashSet<Address>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Address> Addresses
        {
            get { return this.addresses; }
            set { this.addresses = value; }
        }
    }
}
