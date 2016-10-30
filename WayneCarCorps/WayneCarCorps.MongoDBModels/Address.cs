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

        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("AddressLine")]
        public string AddressLine { get; set; }

        [BsonElement("CountryId")]
        public int CountryId { get; set; }
    }
}