using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WayneCarCorps.Models
{
    public class Manufacturer
    {
        private IList<Model> models;
        public Manufacturer()
        {
            this.Models = new List<Model>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual int? AddressId { get; set; }

        public virtual Address Address { get; set; }

        public virtual IList<Model> Models
        {
            get { return this.models; }
            set { this.models = value; }
        }
    }
}
