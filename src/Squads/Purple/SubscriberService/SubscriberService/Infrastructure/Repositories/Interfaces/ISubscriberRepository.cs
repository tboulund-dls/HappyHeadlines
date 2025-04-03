using SubscriberService.Domain.Models;

namespace SubscriberService.Infrastructure.Repositories.Interfaces;

public interface ISubscriberRepository
{
    Task<IEnumerable<Subscriber>> GetSubscribersForSubscriptionTypeAsync(SubscriptionType subscriptionType);
    Task<Subscriber> CreateSubscriberAsync(Subscriber subscriber);
    Task<Subscriber?> GetSubscriberByEmailAsync(string email);
    Task DeleteSubscriberAsync(Subscriber subscriber);
}