using MongoDB.Bson.Serialization.Attributes;

namespace infrastrucure.Models;

public class WordDbModel
{
    [BsonId]
    public string Word { get; set; }
}