using infrastrucure.Models;
using MongoDB.Driver;

namespace infrastrucure.Context;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;
    public readonly IMongoCollection<WordDbModel> Words;

    public MongoDbContext(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
        Words = _database.GetCollection<WordDbModel>("Words");
    }
}