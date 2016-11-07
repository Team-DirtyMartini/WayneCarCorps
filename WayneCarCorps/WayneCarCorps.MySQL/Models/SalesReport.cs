namespace WayneCarCorps.MySQL.Models
{
    public class SalesReport
    {
        public int CarModelId { get; set; }

        public string CarModel { get; set; }

        public string Manufacturer { get; set; }

        public int Seats { get; set; }

        public string Dealer { get; set; }

        public decimal TotalSum { get; set; }

        public int SoldCars { get; set; }
    }
}
