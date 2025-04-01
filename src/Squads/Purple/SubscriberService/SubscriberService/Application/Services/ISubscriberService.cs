using SubscriberService.Domain.Dtos;
using SubscriberService.Models;

namespace SubscriberService.Application.Services;

public interface ISubscriberService
{
    Task<List<Subscriber>> GetSubscribersForSubscriptionTypeAsync(string subscriptionType);
    Task<List<Subscription>> GetSubscriptionsByEmailAsync(string email);
    Task<Subscription> SubscribeAsync(CreateSubscriptionDto createSubscriptionDto);
    Task UnsubscribeAsync(Guid subscriptionId);
}