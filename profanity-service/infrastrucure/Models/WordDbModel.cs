using MongoDB.Bson;

namespace infrastrucure.Models;

public class WordDbModel
{
    public ObjectId Id { get; set; }
    public string Word { get; set; }
}