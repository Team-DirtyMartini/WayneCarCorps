using MongoDB.Driver;
using System.Collections.Generic;
using WayneCarCorps.MongoDBModels.Interfaces;

namespace MongoDBOperator
{
    public class MongoDBLoader<T>
    {
        private MongoDBConnector connector = new MongoDBConnector();
        private IMongoDatabase database;
        private IMongoClient client;

        public MongoDBLoader()
        {
            client = connector.Connect();
            database = connector.GetDatabbase(client);
        }

        public void LoadEntities(IEnumerable<T> collectionToInsert, string collectionName)
        {
            var collection = database.GetCollection<T>(collectionName);

            collection.InsertMany(collectionToInsert);
        }
    }
}
