using System;

namespace WayneCarCorps.Models
{
    public class Sale
    {
        public int Id { get; set; }

        public virtual int CarId { get; set; }

        public virtual Car Car { get; set; }

        public int SoldCars { get; set; }

        public decimal PricePerCar { get; set; }

        public decimal IncomeFromCar { get; set; }

        public virtual int? DealerId { get; set; }

        public virtual Dealer Dealer { get; set; }

        public DateTime Date { get; set; }
    }
}
