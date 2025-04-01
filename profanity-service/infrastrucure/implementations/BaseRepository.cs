using infrastrucure.interfaces;

namespace infrastrucure.implementations;

public class BaseRepository : IRepository
{
    public Task<IEnumerable<string>> getWordds()
    {
        throw new NotImplementedException();
    }

    public Task<string> Clean(string message)
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