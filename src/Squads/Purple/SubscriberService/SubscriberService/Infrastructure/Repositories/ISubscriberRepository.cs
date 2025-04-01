using SubscriberService.Models;

namespace SubscriberService.Infrastructure.Repositories;

public interface ISubscriberRepository
{
    Task<IEnumerable<Subscriber>> GetSubscribersForSubscriptionTypeAsync(SubscriberType subscriberType);
    Task<Subscriber> CreateSubscriberAsync(Subscriber subscriber);
    Task<Subscriber?> GetSubscriberByEmailAsync(string email);
    Task DeleteSubscriberAsync(Subscriber subscriber);
}