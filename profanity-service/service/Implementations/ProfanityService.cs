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
        return await _repository.GetWords();
    }

    public async Task<string> Clean(string message)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> AddWord(string word)
    {
        return await _repository.AddWord(word);
    }

    public async Task<bool> DeleteWord(int id)
    {
        return await _repository.DeleteWord(id);
    }
}