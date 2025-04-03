﻿using service.Models;

namespace service.Interfaces;

public interface IService
{
    Task<IEnumerable<WordModel>> getWords();
    Task<string> Clean(string message);
    Task<bool> AddWord(WordModel word);
    Task<bool> DeleteWord(string id);

}