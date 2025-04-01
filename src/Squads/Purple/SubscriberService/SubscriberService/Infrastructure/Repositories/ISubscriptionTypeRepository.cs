using SubscriberService.Domain.Models;

namespace SubscriberService.Infrastructure.Repositories;

public interface ISubscriptionTypeRepository
{
    Task<SubscriptionType?> GetSubscriptionTypeByNameAsync(string name);
}