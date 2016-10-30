using MongoDB.Driver;

namespace MongoDBOperator
{
    public class Connector
    {
        public const string MongoDBConnectionString = "mongodb://localhost";
        public const string MongoDatabaseName = "WayneCarCorps";

        public IMongoClient Connect()
        {
            MongoClient client = new MongoClient(MongoDBConnectionString);

            return client;
        }

        public IMongoDatabase GetDatabbase(IMongoClient client, string databaseName )
        {
            IMongoDatabase database = client.GetDatabase(databaseName);

            return database;
        }
    }
}
