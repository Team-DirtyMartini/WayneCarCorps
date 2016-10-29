using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using WayneCarCorps.MongoDBModels.Interfaces;

namespace WayneCarCorps.MongoDBModels
{
    public class Manufacturer : IMongoModel
    {
        public Manufacturer(string name, int addressId)
        {
            this.Id = ObjectId.GenerateNewId().ToString();
            this.Name = name;
            this.AddressId = addressId;
        }

        [BsonElement("_id")]
        public string Id { get; set; }

        public string Name { get; set; }

        public int AddressId { get; set; }
    }
}
