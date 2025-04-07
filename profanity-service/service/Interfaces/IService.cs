using service.Models;

namespace service.Interfaces;

public interface IService
{
    Task<IEnumerable<WordModel>> getWords();
    Task<string> Clean(string message);
    Task<WordModel> AddWord(string word);
    Task<bool> DeleteWord(string id);

}