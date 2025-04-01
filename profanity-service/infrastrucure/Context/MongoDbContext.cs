using infrastrucure.Models;
using MongoDB.Driver;

namespace infrastrucure.Context;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;
    public readonly IMongoCollection<WordDbModel> Words;
}