using infrastrucure.interfaces;
using service.Interfaces;
using service.Models;

namespace service;

public class ProfanityService : IService
{
    private readonly IRepository _repository;
    private readonly ICacheRepository _cacheRepository;

    public ProfanityService(IProfanityServiceArgs args)
    {
        _repository = args.Repository;
        _cacheRepository = args.CacheRepository;
    }

    public async Task<IEnumerable<WordModel>> getWords()
    {
        return await _repository.GetWords();
    }

    public async Task<string> Clean(string message)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> AddWord(WordModel word)
    {
        return await _repository.AddWord(word);
    }

    public async Task<bool> DeleteWord(string id)
    {
        return await _repository.DeleteWord(id);
    }
}