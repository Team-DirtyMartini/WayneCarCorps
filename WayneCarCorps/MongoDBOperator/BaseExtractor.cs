using MongoDB.Driver;
using System.Collections.Generic;
using WayneCarCorps.MongoDBModels;
using WayneCarCorps.MongoDBModels.Interfaces;

namespace MongoDBOperator
{
    public abstract class BaseExtractor<T> where T : IMongoModel
    {
        private Connector connector = new Connector();
        private IMongoDatabase database;
        private IMongoClient client;

        protected BaseExtractor()
        {
            client = connector.Connect();
            database = connector.GetDatabbase(client);
        }

        public IEnumerable<T> GetCarsCollection(string collectionType)
        {
            var entity = database.GetCollection<T>(collectionType);

            var entityCollection = entity.Find(e => true).ToEnumerable();

            return entityCollection;
        }
    }
}
