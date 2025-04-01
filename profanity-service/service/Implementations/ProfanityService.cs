using infrastrucure.interfaces;
using service.Interfaces;

namespace service;

public class ProfanityService : IService
{
    private readonly IRepository _repository;

    public ProfanityService(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<string>> getWords()
    {
        throw new NotImplementedException();
    }

    public async Task<string> Clean(string message)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> AddWord(string word)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteWord(int id)
    {
        throw new NotImplementedException();
    }
}