using System.IO;
using System.Linq;
using Newtonsoft.Json;
using WayneCarCorps.Data;

namespace WayneCarCorps.JsonHandler
{
    public static class JsonWriter
    {
        public static void WriteToJson()
        {
            var dbContext = new WayneCarCorpsContext();

            var salesOfCars = dbContext.Sales
                .GroupBy(x => x.CarId)
                .Select(x => new
                {
                    CarId = x.Key,
                    TotalSum = x.Sum(car => car.PricePerCar),
                    SoldCars = x.Sum(car => car.SoldCars)
                });

            var cars = from model in dbContext.Models
                       join car in dbContext.Cars on model.Id equals car.ModelId
                       join sale in salesOfCars on car.Id equals sale.CarId
                       select new
                       {
                           CarModelId = model.Id,
                           CarModel = model.Name,
                           Manufacturer = model.Manufacturer.Name,
                           Seats = model.NumberOfSeats,
                           Dealer = car.Dealer.Name,
                           TotalSum = sale.TotalSum,
                           SoldCars = sale.SoldCars
                       };

            foreach (var car in cars)
            {
                var serializedObject = JsonConvert.SerializeObject(car, Formatting.Indented);

                using (var writer = new StreamWriter(string.Format("../../../JsonReports/{0}.json", car.CarModelId)))
                {
                    writer.WriteLine(serializedObject);
                }
            }
        }
    }
}
