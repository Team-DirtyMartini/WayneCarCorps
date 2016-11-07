using System.Collections.Generic;

namespace WayneCarCorps.Models
{
    public class Dealer
    {
        private ICollection<Car> cars;
        public Dealer()
        {
            this.Cars = new HashSet<Car>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual int? AddressId { get; set; }

        public virtual Address Address { get; set; }

        public decimal? Incomes { get; set; }

        public decimal? Expenses { get; set; }

        public virtual ICollection<Car> Cars
        {
            get { return this.cars; }
            set { this.cars = value; }
        }
    }
}
