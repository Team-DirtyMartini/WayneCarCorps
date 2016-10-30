using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using WayneCarCorps.MongoDBModels.Interfaces;

namespace WayneCarCorps.MongoDBModels
{
    public class Model : IMongoModel
    {
        public Model(string name, int manufacturerId, int carTypeId, int numberOfSeats)
        {
            this.Id = ObjectId.GenerateNewId().ToString();
            this.Name = name;
            this.ManufacturerId = manufacturerId;
            this.CarTypeId = carTypeId;
            this.NumberOfSeats = numberOfSeats;
        }

        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("manufacturerId")]
        public int ManufacturerId { get; set; }

        [BsonElement("carTypeId")]
        public int CarTypeId { get; set; }

        [BsonElement("numberOfSeats")]
        public int NumberOfSeats { get; set; }
    }
}
