using service.Models;

namespace infrastrucure.interfaces;

public interface ICacheRepository
{
    Task<WordModel?> GetByIdAsync(WordModel word);

    Task<WordModel> CreateOrUpdateAsync(WordModel word);

    Task<bool> DeleteAsync(WordModel word);
}