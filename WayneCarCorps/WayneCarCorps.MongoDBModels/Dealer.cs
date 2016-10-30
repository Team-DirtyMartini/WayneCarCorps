using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using WayneCarCorps.MongoDBModels.Interfaces;

namespace WayneCarCorps.MongoDBModels
{
    public class Dealer : IMongoModel
    {
        public Dealer(string name, int addressId, decimal incomes, decimal expenses, int carId, int quantity)
        {
            this.Id = ObjectId.GenerateNewId().ToString();
            this.Name = name;
            this.AddressId = addressId;
            this.Incomes = incomes;
            this.Expenses = expenses;
        }

        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("addressId")]
        public int AddressId { get; set; }

        [BsonElement("incomes")]
        public decimal Incomes { get; set; }

        [BsonElement("expenses")]
        public decimal Expenses { get; set; }
    }
}
