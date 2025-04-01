using infrastrucure.interfaces;

namespace infrastrucure.implementations;

public class MongoDbRepository : IRepository
{
    public Task<IEnumerable<string>> GetWords()
    {
        throw new NotImplementedException();
    }

    public Task<string> Lookup(string message)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AddWord(string word)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteWord(int id)
    {
        throw new NotImplementedException();
    }
}