using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using WayneCarCorps.MongoDBModels.Interfaces;

namespace WayneCarCorps.MongoDBModels
{
    public class MongoCar : IMongoModel
    {
        public MongoCar(string manufacturer, string model, string type, int year, string colour, int power, int numberOfSeats, decimal price, string dealer)
        {
            this.Manufacturer = manufacturer;
            this.Model = model;
            this.Type = type;
            this.Year = year;
            this.Colour = colour;
            this.Power = power;
            this.NumberOfSeats = numberOfSeats;
            this.Price = price;
            this.Dealer = dealer;
        }

        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("manufacturer")]
        public string Manufacturer { get; set; }

        [BsonElement("model")]
        public string Model { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("year")]
        public int Year { get; set; }

        [BsonElement("colour")]
        public string Colour { get; set; }

        [BsonElement("power")]
        public int Power { get; set; }

        [BsonElement("numberOfSeats")]
        public int NumberOfSeats { get; set; }

        [BsonElement("price")]
        public decimal Price { get; set; }

        [BsonElement("dealer")]
        public string Dealer { get; set; }
    }
}
