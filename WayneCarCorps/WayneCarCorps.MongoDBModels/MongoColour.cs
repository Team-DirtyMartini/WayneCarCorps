﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using WayneCarCorps.MongoDBModels.Interfaces;

namespace WayneCarCorps.MongoDBModels
{
    public class MongoColour : IMongoModel
    {
        public MongoColour(string name)
        {
            this.Id = ObjectId.GenerateNewId().ToString();
            this.Name = name;
        }

        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }
    }
}
