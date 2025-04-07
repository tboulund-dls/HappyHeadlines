using System.Text.RegularExpressions;
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
        try
        {
            IEnumerable<WordModel> words = await _repository.GetWords();
            words = words.ToList();
            if (words.Count() > 0 && words is not null)
            {
                foreach (var word in words)
                {
                    await _cacheRepository.CreateOrUpdateAsync(word);
                }
                return words;
            }
            return null;
        }
        catch (Exception e)
        {
            throw new Exception("Error while trying to get words");
        }
    }

    public async Task<string> Clean(string message)
    {
        try
        {       
            string[] words = Regex.Split(message, @"\W+");
            Dictionary<string, string> foundWords = new Dictionary<string, string>();
            var result = message;
            // Checks if words exist in the cache or database
            foreach (var word in words)
            {
                var wordModel = new WordModel
                {
                    Word = word,
                };
                WordModel? cacheWord = await _cacheRepository.GetByIdAsync(wordModel);
                if (cacheWord is not null)
                {
                    foundWords.Add(cacheWord.Word, new string('*',word.Length));
                }
                WordModel? dbWord = await _repository.Lookup(wordModel.Word);
                if (dbWord is not null && cacheWord is null)
                {
                    await _repository.Lookup(dbWord.Word);
                    foundWords.Add(dbWord.Word, new string('*', word.Length));
                }
            }
            
            // Cleans all words
            foreach (var fw in foundWords)
            {
                var pattern = Regex.Escape(fw.Key);
                var regex = new Regex(pattern, RegexOptions.IgnoreCase);
                if (result.Contains(fw.Key))
                {
                    result = regex.Replace(fw.Value, pattern);
                }
            }
            return result;
        }
        catch (Exception e)
        {
            throw new Exception("Error while trying to clean message");
        }
    }
    

    public async Task<WordModel> AddWord(string word)
    {
        try
        {
            var wordModel = new WordModel
            {
                Word = word,
            };
            var response = await _repository.AddWord(wordModel);
            if (response)
            {
                await _cacheRepository.CreateOrUpdateAsync(wordModel);
                return wordModel;
            }
            return null;
        }
        catch (Exception e)
        {
            throw new Exception("Error while trying to add new word");
        }
    }

    public async Task<bool> DeleteWord(string id)
    {
        try
        {
            var response = await _repository.DeleteWord(id);
            if (response)
            {
                var word = new WordModel { Word = id };
                await _cacheRepository.DeleteAsync(word);
                return response;
            }
            return false;
        }
        catch (Exception e)
        {
            throw new Exception("Error while trying to delete word");
        }
    }
}