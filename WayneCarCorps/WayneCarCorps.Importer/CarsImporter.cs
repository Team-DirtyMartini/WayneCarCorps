using MongoDBOperator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using WayneCarCorps.Data;
using WayneCarCorps.Models;
using WayneCarCorps.MongoDBModels;

namespace WayneCarCorps.Importer
{
   public class CarsImporter
    {
        private const int DefaultNumberOfSeats = 4;

        private readonly WayneCarCorpsContext db;
        public CarsImporter(WayneCarCorpsContext db)
        {
            this.db = db;
            
        }

        public void import(IEnumerable<MongoCar> cars)
        {
           // var counter = 0;
            this.db.Configuration.AutoDetectChangesEnabled = false;
            this.db.Configuration.ValidateOnSaveEnabled = false;
            foreach (var car in cars)
            {
                db.Cars.Add(CreateCar(car));
            }

            this.db.Configuration.AutoDetectChangesEnabled = true;
            this.db.Configuration.ValidateOnSaveEnabled = true;
            db.SaveChanges();
        }

        private Car CreateCar(MongoCar car)
        {
            var newCar = new Car
            {
                ColorId = GetCoulourId(car.Colour),
                ModelId = GetModelId(car.Model, car.Manufacturer,car.Type, car.NumberOfSeats),
                Year = car.Year,
                Power = car.Power,
                Price = car.Price,
                DealerId = GetDealerId(car.Dealer)
                
            };

            return newCar;
        }
        private int GetDealerId(string dealerName)
        {
            var id = this.db.Dealers.Where(x => x.Name == dealerName).Select(x => x.Id).FirstOrDefault();

            if (id == 0)
            {
                var newDealer = new Dealer
                {
                    Name = dealerName,
                    
                };

                this.db.Dealers.Add(newDealer);
                this.db.SaveChanges();

                id = this.db.Dealers.Where(x => x.Name == dealerName).Select(x => x.Id).First();
            }

            return id;
        }
        private int GetModelId(string modelName, string manufacturerName, string cartypeName, int numberOfSeats)
        {
            var id = this.db.Models.Where(x => x.Name == modelName).Select(x => x.Id).FirstOrDefault();

            if (id == 0)
            {
                var newModel = new Model
                {
                    Name = modelName,
                    CarTypeId = GetCartypeId(cartypeName),
                    NumberOfSeats = numberOfSeats,
                    ManufacturerId = GetManufacturerId(manufacturerName)                    
                };

                this.db.Models.Add(newModel);
                db.SaveChanges();
                id = this.db.Models.Where(x => x.Name == modelName).Select(x => x.Id).First();
            }

            return id;
        }

        private int GetManufacturerId(string manufacturerName)
        {
            var id = this.db.Manufacturers.Where(x => x.Name == manufacturerName).Select(x => x.Id).FirstOrDefault();

            if (id == 0)
            {
                var newManufacturer = new Manufacturer
                {
                    Name = manufacturerName,
                    AddressId = null
                };

                this.db.Manufacturers.Add(newManufacturer);
                this.db.SaveChanges();

                id = this.db.Manufacturers.Where(x => x.Name == manufacturerName).Select(x => x.Id).First();
            }

            return id;
        }

        private int GetCartypeId(string cartypeName)
        {
            var id = this.db.CarTypes.Where(x => x.Name == cartypeName).Select(x => x.Id).FirstOrDefault();

            if (id == 0)
            {
                var newCartype = new CarType
                {
                    Name = cartypeName
                };
                this.db.CarTypes.Add(newCartype);
                db.SaveChanges();

                id = this.db.CarTypes.Where(x => x.Name == cartypeName).Select(x => x.Id).First();
            }

            return id;
        }

        private int GetCoulourId(string colour)
        {
            var Id = this.db.Colors.Where(x => x.Name == colour).Select(x => x.Id).FirstOrDefault();

            if (Id == 0)
            {
                var newColor = new Color
                {
                    Name = colour
                };

                this.db.Colors.Add(newColor);
                this.db.SaveChanges();

                Id = this.db.Colors.Where(x => x.Name == colour).Select(x => x.Id).First();
            }

            return Id;
        }

        public static T DeepClone<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }
    }
}
