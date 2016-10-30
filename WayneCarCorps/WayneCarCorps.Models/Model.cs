using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WayneCarCorps.Models
{
    public class Model
    {
        private ICollection<Car> cars;
        public Model()
        {
            this.Cars = new HashSet<Car>();
        }

        public int Id { get; set; }
        
        [StringLength(50)]
        public string Name { get; set; }

        public virtual int ManufacturerId { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public virtual int CarTypeId { get; set; }

        public virtual CarType CarType { get; set; }

        public virtual ICollection<Car> Cars
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
