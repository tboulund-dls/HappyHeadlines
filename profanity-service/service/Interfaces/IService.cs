namespace service.Interfaces;

public interface IService
{
    Task<IEnumerable<string>> getWordds();
    Task<string> Clean(string message);
    Task<bool> AddWord(string word);
    Task<bool> DeleteWord(int id);

}