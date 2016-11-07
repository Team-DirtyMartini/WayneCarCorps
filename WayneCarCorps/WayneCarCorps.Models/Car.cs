using System;

namespace WayneCarCorps.Models
{
    [Serializable]
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
    }
}
