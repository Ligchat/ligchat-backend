using MongoDB.Driver;
using tests_.src.Domain.Entities;

namespace tests_.src.Application.Common.Utilities
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        // Coleção para FlowWhatsapp
        public IMongoCollection<FlowWhatsapp> FlowsWhatsapp
        {
            get
            {
                return _database.GetCollection<FlowWhatsapp>("flowsWhatsapp");
            }
        }
    }
}
