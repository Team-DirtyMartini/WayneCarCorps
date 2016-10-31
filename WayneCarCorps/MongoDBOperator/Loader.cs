using MongoDB.Driver;
using System.Collections.Generic;
using WayneCarCorps.MongoDBModels.Interfaces;

namespace MongoDBOperator
{
    public class Loader<T>
    {
        private Connector connector = new Connector();
        private IMongoDatabase database;
        private IMongoClient client;

        public Loader()
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
