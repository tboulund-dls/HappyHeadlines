using SubscriberService.Models;

namespace SubscriberService.Infrastructure.Repositories;

public interface ISubscriptionRepository
{
    Task<IEnumerable<Subscription>> GetSubscriptionsByEmailAsync(string email);
    Task<Subscription?> GetSubscriptionByIdAsync(Guid subscriptionId);
    Task<Subscription> SubscribeAsync(Subscription subscription);
    Task UnsubscribeAsync(Subscription subscription);
    Task<bool> DoesUserAlreadyHaveSubscriptionAsync(string email, string subscriptionType);
}