using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WayneCarCorps.Models
{
    public class Dealer
    {
        private IList<Car> cars;
        public Dealer()
        {
            this.Cars = new List<Car>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual int? AddressId { get; set; }

        public virtual Address Address { get; set; }

        public decimal? Incomes { get; set; }

        public decimal? Expenses { get; set; }

        public virtual IList<Car> Cars
        {
            get { return this.cars; }
            set { this.cars = value; }
        }
    }
}
