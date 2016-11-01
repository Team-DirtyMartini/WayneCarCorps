using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using WayneCarCorps.MongoDBModels.Interfaces;

namespace WayneCarCorps.MongoDBModels
{
    public class MongoAddress : IMongoModel
    {
        public MongoAddress(string addressLine, int countryId)
        {
            this.Id = ObjectId.GenerateNewId().ToString();
            this.AddressLine = addressLine;
            this.CountryId = countryId;
        }

        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("addressLine")]
        public string AddressLine { get; set; }

        [BsonElement("countryId")]
        public int CountryId { get; set; }
    }
}