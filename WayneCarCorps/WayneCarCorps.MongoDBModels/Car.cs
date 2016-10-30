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

        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("model")]
        public string Model { get; set; }

        [BsonElement("manufacturerId")]
        public int ManufacturerId { get; set; }

        [BsonElement("year")]
        public int Year { get; set; }

        [BsonElement("price")]
        public decimal Price { get; set; }

        [BsonElement("carTypeId")]
        public int CarTypeId { get; set; }

        [BsonElement("power")]
        public int Power { get; set; }

        [BsonElement("colourId")]
        public int ColourId { get; set; }

        [BsonElement("numberOfSeats")]
        public int NumberOfSeats { get; set; }
    }
}
