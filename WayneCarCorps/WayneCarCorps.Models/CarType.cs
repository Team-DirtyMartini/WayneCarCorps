using System.Collections.Generic;

namespace WayneCarCorps.Models
{
    public class CarType
    {
        private ICollection<Model> models;
        public CarType()
        {
            this.Models = new HashSet<Model>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Model> Models
        {
            get { return this.models; }
            set { this.models = value; }
        }
    }
}
