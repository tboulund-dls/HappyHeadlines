using MassTransit;
using Serilog;
using SharedModels;
using SubscriberService.Domain.Dtos;
using SubscriberService.Domain.Exceptions;
using SubscriberService.Domain.Models;
using SubscriberService.Infrastructure.Repositories;

namespace SubscriberService.Application.Services;

public class SubscriberService(
    ISubscriberRepository subscriberRepository,
    ISubscriptionRepository subscriptionRepository,
    ISubscriptionTypeRepository subscriptionTypeRepository,
    IPublishEndpoint publishEndpoint
) : ISubscriberService
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
            Log.Information("Creating new subscriber with email: {Email}", createSubscriptionDto.Email);
            subscriber = new Subscriber
            {
                Id = Guid.NewGuid(),
                Email = createSubscriptionDto.Email
            };
            await subscriberRepository.CreateSubscriberAsync(subscriber);
        }

        var subscriptionTypeEntity =
            await subscriptionTypeRepository.GetSubscriptionTypeByNameAsync(createSubscriptionDto.SubscriptionType);
        if (subscriptionTypeEntity == null)
        {
            throw new NotFoundException("Subscription type not found");
        }

        if (await subscriptionRepository.DoesUserAlreadyHaveSubscriptionAsync(createSubscriptionDto.Email,
                createSubscriptionDto.SubscriptionType))
        {
            throw new BadRequestException("User already has this subscription");
        }

        var subscription = new Subscription
        {
            Id = Guid.NewGuid(),
            SubscriptionTypeId = subscriptionTypeEntity.Id,
            SubscriberId = subscriber.Id
        };

        Log.Information("Creating new subscription for email: {Email}, SubscriptionType: {SubscriptionType}",
            createSubscriptionDto.Email, createSubscriptionDto.SubscriptionType);
        var result = await subscriptionRepository.SubscribeAsync(subscription);
        
        Log.Information("Publishing SubscriptionCreatedEvent for email: {Email}, SubscriptionType: {SubscriptionType}",
            createSubscriptionDto.Email, createSubscriptionDto.SubscriptionType);
        await publishEndpoint.Publish(new SubscriptionCreatedEvent
        {
            Email = subscriber.Email,
            SubscriptionType = subscriptionTypeEntity.Type,
        });
        
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

        var remainingSubscriptions =
            await subscriptionRepository.GetSubscriptionsByEmailAsync(subscription.Subscriber!.Email);
        if (!remainingSubscriptions.Any())
        {
            Log.Information("Deleting subscriber with email: {Email}", subscription.Subscriber.Email);
            await subscriberRepository.DeleteSubscriberAsync(subscription.Subscriber);
        }
    }
}