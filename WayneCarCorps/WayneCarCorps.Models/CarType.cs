using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
