using service.Models;

namespace infrastrucure.interfaces;

public interface IRepository
{
    Task<IEnumerable<WordModel>> GetWords();
    Task<WordModel> Lookup(string word);
    Task<bool> AddWord(WordModel word);
    Task<bool> DeleteWord(string id);
}