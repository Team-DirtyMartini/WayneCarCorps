using MongoDB.Driver;
using System.Collections.Generic;
using WayneCarCorps.MongoDBModels.Interfaces;

namespace MongoDBOperator
{
    public class MongoDBExtractor<T> where T : IMongoModel
    {
        private MongoDBConnector connector = new MongoDBConnector();
        private IMongoDatabase database;
        private IMongoClient client;

        public MongoDBExtractor()
        {
            client = connector.Connect();
            database = connector.GetDatabbase(client);
        }

        public IEnumerable<T> GetEntitiesCollection(string collectionType)
        {
            var entities = database.GetCollection<T>(collectionType);

            var entitiesCollection = entities.Find(e => true).ToEnumerable();

            return entitiesCollection;
        }
    }
}
