using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using WayneCarCorps.MongoDBModels.Interfaces;

namespace WayneCarCorps.MongoDBModels
{
    public class Car : IMongoModel
    {
        public Car(string model, int manufacturerId, int year, decimal price, int carTypeId, int power, int colourId, int numberOfSeats)
        {
            this.Id = ObjectId.GenerateNewId().ToString();
            this.Model = model;
            this.ManufacturerId = manufacturerId;
            this.Year = year;
            this.Price = price;
            this.CarTypeId = carTypeId;
            this.Power = power;
            this.ColourId = colourId;
            this.NumberOfSeats = numberOfSeats;
        }

        [BsonElement("_id")]
        public string Id { get; set; }

        public string Model { get; set; }

        public int ManufacturerId { get; set; }

        public int Year { get; set; }

        public decimal Price { get; set; }

        public int CarTypeId { get; set; }

        public int Power { get; set; }

        public int ColourId { get; set; }

        public int NumberOfSeats { get; set; }
    }
}
