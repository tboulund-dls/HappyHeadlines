using SubscriberService.Models;

namespace SubscriberService.Infrastructure.Repositories;

public interface ISubscriberRepository
{
    Task<IEnumerable<Subscriber>> GetSubscribersForSubscriptionTypeAsync(SubscriberType subscriberType);
    Task<IEnumerable<Subscription>> GetSubscriptionsByEmailAsync(string email);
    Task<Subscription> SubscribeAsync(Subscription subscription);
    Task UnsubscribeAsync(Subscription subscription);
}