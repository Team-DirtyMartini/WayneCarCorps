using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WayneCarCorps.Models
{
    public class Car
    {
        public int Id { get; set; }

        public virtual int ModelId { get; set; }

        public virtual Model Model { get; set; }

        public int Year { get; set; }

        public decimal Price { get; set; }            
      
        public int Power { get; set; }

        public virtual int ColorId { get; set; }

        public virtual Color Color { get; set; }

        public virtual int DealerId { get; set; }

        public virtual Dealer Dealer { get; set; }

        [Column("Number of seats")]
        public int NumberOfSeats { get; set; }
    }
}
