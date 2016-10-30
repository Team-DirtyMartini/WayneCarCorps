using MongoDB.Driver;
using System.Collections.Generic;
using WayneCarCorps.MongoDBModels.Interfaces;

namespace MongoDBOperator
{
    public class Extractor<T> where T : IMongoModel
    {
        private Connector connector = new Connector();
        private IMongoDatabase database;
        private IMongoClient client;

        public Extractor()
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
