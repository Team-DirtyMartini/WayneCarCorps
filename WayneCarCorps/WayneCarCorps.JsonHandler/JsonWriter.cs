using System.IO;
using System.Linq;
using Newtonsoft.Json;
using WayneCarCorps.Data;
using WayneCarCorps.Data.Common;
using WayneCarCorps.Models;

namespace WayneCarCorps.JsonHandler
{
    public class JsonWriter
    {
        private const string JsonFilesPath = "../../../JsonReports/{0}.json";
        private IRepository<Sale> repositorySales; 
        private IRepository<Model> repositoryModels;
        private IRepository<Car> repositoryCars;

        public JsonWriter(
            IRepository<Sale> repositorySales,
            IRepository<Model> repositoryModels,
            IRepository<Car> repositoryCars)
        {
            this.repositorySales = repositorySales;
            this.repositoryModels = repositoryModels;
            this.repositoryCars = repositoryCars;
        }

        public void WriteToJson()
        {
            var dbContext = new WayneCarCorpsContext();

            var salesOfCars = this.repositorySales.All()
                .GroupBy(x => x.CarId)
                .Select(x => new
                {
                    CarId = x.Key,
                    TotalSum = x.Sum(car => car.PricePerCar),
                    SoldCars = x.Sum(car => car.SoldCars)
                });

            var cars = from model in this.repositoryModels.All()
                       join car in this.repositoryCars.All() on model.Id equals car.ModelId
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

                using (var writer = new StreamWriter(string.Format(JsonFilesPath, car.CarModelId)))
                {
                    writer.WriteLine(serializedObject);
                }
            }
        }
    }
}
