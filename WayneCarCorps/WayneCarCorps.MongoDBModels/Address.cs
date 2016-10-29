using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using WayneCarCorps.MongoDBModels.Interfaces;

namespace WayneCarCorps.MongoDBModels
{
    public class Address : IMongoModel
    {
        public Address(string addressLine, int countryId)
        {
            this.Id = ObjectId.GenerateNewId().ToString();
            this.AddressLine = addressLine;
            this.CountryId = countryId;
        }

        [BsonElement("_id")]
        public string Id { get; set; }

        public string AddressLine { get; set; }

        public int CountryId { get; set; }
    }
}