using SubscriberService.Domain.Models;

namespace SubscriberService.Infrastructure.Repositories.Interfaces;

public interface ICacheRepository
{
    Task<Subscriber?> GetSubscriberByEmailAsync(string email);
    Task IndexSubscriberAsync(Subscriber subscriber);
    Task DeleteSubscriberAsync(string email);
    
    Task<SubscriptionType?> GetSubscriptionTypeByNameAsync(string name);
    Task IndexSubscriptionTypeAsync(SubscriptionType subscriptionType);
    Task DeleteSubscriptionTypeAsync(string name);
}