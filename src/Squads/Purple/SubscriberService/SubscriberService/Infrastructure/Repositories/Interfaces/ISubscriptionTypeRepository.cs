using SubscriberService.Domain.Models;

namespace SubscriberService.Infrastructure.Repositories.Interfaces;

public interface ISubscriptionTypeRepository
{
    Task<SubscriptionType?> GetSubscriptionTypeByNameAsync(string name);
}