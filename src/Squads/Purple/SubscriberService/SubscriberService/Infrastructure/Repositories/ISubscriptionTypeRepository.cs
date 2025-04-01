using SubscriberService.Models;

namespace SubscriberService.Infrastructure.Repositories;

public interface ISubscriptionTypeRepository
{
    Task<SubscriberType?> GetSubscriptionTypeByNameAsync(string name);
}