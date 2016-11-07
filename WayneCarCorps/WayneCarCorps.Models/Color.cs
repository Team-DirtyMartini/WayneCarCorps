using System.Collections.Generic;

namespace WayneCarCorps.Models
{
    public class Color
    {
        private ICollection<Car> cars;

        public Color()
        {
            this.Cars = new HashSet<Car>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Car> Cars
        {
            get { return this.cars; }
            set { this.cars = value; }
        }
    }
}
