using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using WayneCarCorps.Data.Common;
using WayneCarCorps.Models;
using WayneCarCorps.MongoDBModels;

namespace WayneCarCorps.Importer
{
    public class CarsImporter
    {
        private const int DefaultNumberOfSeats = 4;

        private IModelsFactory modelsFactory;
        private IUnitOfWork unitOfWork;
        private IRepository<Car> cars;
        private IRepository<Dealer> dealers;
        private IRepository<Model> models;
        private IRepository<Manufacturer> manufacturers;
        private IRepository<Color> colors;
        private IRepository<CarType> carTypes;

        public CarsImporter(IModelsFactory modelsFactory,
            IUnitOfWork unitOfWork,
            IRepository<Car> cars,
            IRepository<Dealer> dealers,
            IRepository<Model> models,
            IRepository<Manufacturer> manufacturers,
            IRepository<Color> colors,
            IRepository<CarType> carTypes)
        {
            this.modelsFactory = modelsFactory;
            this.unitOfWork = unitOfWork;
            this.cars = cars;
            this.carTypes = carTypes;
            this.colors = colors;
            this.dealers = dealers;
            this.manufacturers = manufacturers;
            this.models = models;
        }

        public void import(IEnumerable<MongoCar> cars)
        {
            //var counter = 0;
            //this.db.Configuration.AutoDetectChangesEnabled = false;
            //this.db.Configuration.ValidateOnSaveEnabled = false;
            foreach (var car in cars)
            {
                this.cars.Add(CreateCar(car));
                //counter++;
                //if (counter % 20 == 0)
                //{
                //    this.db.SaveChanges();
                //    this.db = new WayneCarCorpsContext();
                //    this.db.Configuration.AutoDetectChangesEnabled = false;
                //    this.db.Configuration.ValidateOnSaveEnabled = false;
                //}
            }

            //this.db.Configuration.AutoDetectChangesEnabled = true;
            //this.db.Configuration.ValidateOnSaveEnabled = true;
            this.unitOfWork.Commit();
        }

        private Car CreateCar(MongoCar car)
        {
            var newCar = this.modelsFactory.CreateCar();

            newCar.ColorId = GetCoulourId(car.Colour);
            newCar.ModelId = GetModelId(car.Model, car.Manufacturer, car.Type, car.NumberOfSeats);
            newCar.Year = car.Year;
            newCar.Power = car.Power;
            newCar.Price = car.Price;
            newCar.DealerId = GetDealerId(car.Dealer);

            return newCar;
        }
        private int GetDealerId(string dealerName)
        {
            var id = this.dealers.All().Where(x => x.Name == dealerName).Select(x => x.Id).FirstOrDefault();

            if (id == 0)
            {
                var newDealer = this.modelsFactory.CreateDealer();
                newDealer.Name = dealerName;

                this.dealers.Add(newDealer);
                this.unitOfWork.Commit();

                id = this.dealers.All().Where(x => x.Name == dealerName).Select(x => x.Id).First();
            }

            return id;
        }
        private int GetModelId(string modelName, string manufacturerName, string cartypeName, int numberOfSeats)
        {
            var id = this.models.All().Where(x => x.Name == modelName).Select(x => x.Id).FirstOrDefault();

            if (id == 0)
            {
                var newModel = this.modelsFactory.CreateModel();
                newModel.Name = modelName;
                newModel.CarTypeId = GetCartypeId(cartypeName);
                newModel.NumberOfSeats = numberOfSeats;
                newModel.ManufacturerId = GetManufacturerId(manufacturerName);               

                this.models.Add(newModel);
                unitOfWork.Commit();
                id = this.models.All().Where(x => x.Name == modelName).Select(x => x.Id).First();
            }

            return id;
        }

        private int GetManufacturerId(string manufacturerName)
        {
            var id = this.manufacturers.All().Where(x => x.Name == manufacturerName).Select(x => x.Id).FirstOrDefault();

            if (id == 0)
            {
                var newManufacturer = this.modelsFactory.CreateManufacturer();
                newManufacturer.Name = manufacturerName;
                newManufacturer.AddressId = null;
                this.manufacturers.Add(newManufacturer);
                this.unitOfWork.Commit();

                id = this.manufacturers.All().Where(x => x.Name == manufacturerName).Select(x => x.Id).First();
            }

            return id;
        }

        private int GetCartypeId(string cartypeName)
        {
            var id = this.carTypes.All().Where(x => x.Name == cartypeName).Select(x => x.Id).FirstOrDefault();

            if (id == 0)
            {
                var newCartype = this.modelsFactory.CreateCarType();
                newCartype.Name = cartypeName;
              
                this.carTypes.Add(newCartype);
                this.unitOfWork.Commit();

                id = this.carTypes.All().Where(x => x.Name == cartypeName).Select(x => x.Id).First();
            }

            return id;
        }

        private int GetCoulourId(string colour)
        {
            var Id = this.colors.All().Where(x => x.Name == colour).Select(x => x.Id).FirstOrDefault();

            if (Id == 0)
            {
                var newColor = this.modelsFactory.CreateColor();
                newColor.Name = colour;             

                this.colors.Add(newColor);
                this.unitOfWork.Commit();

                Id = this.colors.All().Where(x => x.Name == colour).Select(x => x.Id).First();
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
