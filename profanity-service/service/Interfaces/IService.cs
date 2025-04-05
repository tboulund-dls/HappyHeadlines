﻿namespace service.Interfaces;

public interface IService
{
    Task<IEnumerable<string>> getWords();
    Task<string> Clean(string message);
    Task<bool> AddWord(string word);
    Task<bool> DeleteWord(string id);

}