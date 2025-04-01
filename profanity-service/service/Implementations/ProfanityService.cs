using service.Interfaces;

namespace service;

public class ProfanityService : IService
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