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
        return await _repository.getWords();
    }

    public async Task<string> Clean(string message)
    {
        return await _repository.Clean(message);
    }

    public async Task<bool> AddWord(string word)
    {
        return await _repository.AddWord(word);
    }

    public async Task<bool> DeleteWord(string id)
    {
        throw new NotImplementedException();
    }
}