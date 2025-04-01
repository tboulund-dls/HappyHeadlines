using SubscriberService.Domain.Dtos;
using SubscriberService.Domain.Exceptions;
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
            throw new NotFoundException("Subscription type not found");
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
        var subscriber = await subscriberRepository.GetSubscriberByEmailAsync(createSubscriptionDto.Email);
        if (subscriber is null)
        {
            subscriber = new Subscriber
            {
                Id = Guid.NewGuid(),
                Email = createSubscriptionDto.Email
            };
            await subscriberRepository.CreateSubscriberAsync(subscriber);
        }
        
        var subscriptionTypeEntity = await subscriptionTypeRepository.GetSubscriptionTypeByNameAsync(createSubscriptionDto.SubscriptionType);
        if (subscriptionTypeEntity == null)
        {
            throw new NotFoundException("Subscription type not found");
        }
        
        var subscription = new Subscription
        {
            Id = Guid.NewGuid(),
            SubscriptionTypeId = subscriptionTypeEntity.Id,
            SubscriberId = subscriber.Id
        };

        var result = await subscriptionRepository.SubscribeAsync(subscription);
        return result;
    }

    public async Task UnsubscribeAsync(Guid subscriptionId)
    {
        var subscription = await subscriptionRepository.GetSubscriptionByIdAsync(subscriptionId);
        if (subscription == null)
        {
            throw new NotFoundException("Subscription not found");
        }
        
        await subscriptionRepository.UnsubscribeAsync(subscription);

        var remainingSubscriptions = await subscriptionRepository.GetSubscriptionsByEmailAsync(subscription.Subscriber!.Email);
        if (!remainingSubscriptions.Any())
        {
            await subscriberRepository.DeleteSubscriberAsync(subscription.Subscriber);
        }
    }
}