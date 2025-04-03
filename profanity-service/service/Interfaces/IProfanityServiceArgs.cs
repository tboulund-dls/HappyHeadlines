using infrastrucure.interfaces;

namespace service.Interfaces;

public interface IProfanityServiceArgs
{
    IRepository Repository { get; set; }
    ICacheRepository CacheRepository { get; set; }
}