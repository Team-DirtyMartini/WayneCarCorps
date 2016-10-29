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
            this.CarId = carId;
            this.Quantity = quantity;
        }

        [BsonElement("_id")]
        public string Id { get; set; }

        public string Name { get; set; }

        public int AddressId { get; set; }

        public decimal Incomes { get; set; }

        public decimal Expenses { get; set; }

        public int CarId { get; set; }

        public int Quantity { get; set; }
    }
}
