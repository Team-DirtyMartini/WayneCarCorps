using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WayneCarCorps.Models
{
    public class Model
    {
        private IList<Car> cars;
        public Model()
        {
            this.Cars = new List<Car>();
        }

        public int Id { get; set; }
        
        [StringLength(50)]
        public string Name { get; set; }

        public virtual int ManufacturerId { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public virtual int CarTypeId { get; set; }

        public virtual CarType CarType { get; set; }

        [Column("Number of seats")]
        public int NumberOfSeats { get; set; }

        public virtual IList<Car> Cars
        {
            get
            {
                return this.cars;
            }
            set
            {
                this.cars = value;
            }
        }
    }
}
