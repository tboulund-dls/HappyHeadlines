using SubscriberService.Infrastructure.Repositories;
using SubscriberService.Models;

namespace SubscriberService.Application.Services;

public class SubscriberService(ISubscriberRepository subscriberRepository, ISubscriptionRepository subscriptionRepository, ISubscriptionTypeRepository subscriptionTypeRepository) : ISubscriberService
{
    public async Task<List<Subscriber>> GetSubscribersForSubscriptionTypeAsync(string subscriptionType)
    {
        var subscriptionTypeEntity = await subscriptionTypeRepository.GetSubscriptionTypeByNameAsync(subscriptionType);
        if (subscriptionTypeEntity == null)
        {
            throw new Exception("Subscription type not found");
        }
        
        var subscribers = await subscriberRepository.GetSubscribersForSubscriptionTypeAsync(subscriptionTypeEntity);
        return subscribers.ToList();
    }

    public async Task<List<Subscription>> GetSubscriptionsByEmailAsync(string email)
    {
        var subscriptions = await subscriptionRepository.GetSubscriptionsByEmailAsync(email);
        return subscriptions.ToList();
    }

    public async Task<Subscription> SubscribeAsync(CreateSubscriptionDto createSubscriptionDto)
    {
        var subscription = new Subscription
        {
            Id = Guid.NewGuid(),
            TypeId = createSubscriptionDto.SubscriptionType
        };

        var result = await subscriptionRepository.SubscribeAsync(subscription);
        return result;
    }

    public async Task UnsubscribeAsync(Guid subscriptionId)
    {
        var subscription = await subscriptionRepository.GetSubscriptionByIdAsync(subscriptionId);
        if (subscription == null)
        {
            throw new Exception("Subscription not found");
        }

        await subscriptionRepository.UnsubscribeAsync(subscription);
    }
}