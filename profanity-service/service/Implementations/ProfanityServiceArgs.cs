using infrastrucure.interfaces;
using service.Interfaces;

namespace service;

public class ProfanityServiceArgs : IProfanityServiceArgs
{
    public IRepository Repository { get; set; }
    public ICacheRepository CacheRepository { get; set; }
}