﻿using MongoDB.Driver;

namespace MongoDBOperator
{
    public class MongoDBConnector
    {
        public const string MongoDBConnectionString = "mongodb://localhost";
        public const string MongoDatabaseName = "WayneCarCorps";

        public IMongoClient Connect()
        {
            MongoClient client = new MongoClient(MongoDBConnectionString);

            return client;
        }

        public IMongoDatabase GetDatabbase(IMongoClient client)
        {
            IMongoDatabase database = client.GetDatabase(MongoDatabaseName);

            return database;
        }
    }
}
