using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using WayneCarCorps.MongoDBModels.Interfaces;

namespace WayneCarCorps.MongoDBModels
{
    public class Colour : IMongoModel
    {
        public Colour(string name)
        {
            this.Id = ObjectId.GenerateNewId().ToString();
            this.Name = name;
        }

        [BsonElement("_id")]
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
