namespace infrastrucure.interfaces;

public interface IRepository
{
    Task<IEnumerable<string>> getWordds();
    Task<string> Clean(string message);
    Task<bool> AddWord(string word);
    Task<bool> DeleteWord(int id);
}