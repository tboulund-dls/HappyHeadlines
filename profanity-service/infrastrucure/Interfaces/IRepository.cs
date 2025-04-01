namespace infrastrucure.interfaces;

public interface IRepository
{
    Task<IEnumerable<string>> GetWords();
    Task<string> Lookup(string word);
    Task<bool> AddWord(string word);
    Task<bool> DeleteWord(int id);
}