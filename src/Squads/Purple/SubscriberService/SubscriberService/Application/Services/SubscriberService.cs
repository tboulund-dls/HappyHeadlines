using MassTransit;
using Serilog;
using SharedModels;
using SubscriberService.Domain.Dtos;
using SubscriberService.Domain.Exceptions;
using SubscriberService.Domain.Models;
using SubscriberService.Infrastructure.Repositories.Interfaces;

namespace SubscriberService.Application.Services;

public class SubscriberService(
    ISubscriberRepository subscriberRepository,
    ISubscriptionRepository subscriptionRepository,
    ISubscriptionTypeRepository subscriptionTypeRepository,
    IPublishEndpoint publishEndpoint,
    ICacheRepository cacheRepository
) : ISubscriberService
{
    public async Task<List<Subscriber>> GetSubscribersForSubscriptionTypeAsync(string subscriptionType)
    {
        var subscriptionTypeEntity = await GetSubscriptionTypeAsync(subscriptionType);
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
        var subscriber = await EnsureSubscriberExistsAsync(createSubscriptionDto.Email);
        var subscriptionTypeEntity = await GetSubscriptionTypeAsync(createSubscriptionDto.SubscriptionType);

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

        await PublishMessage(new SubscriptionCreatedEvent
        {
            Email = createSubscriptionDto.Email,
            SubscriptionType = createSubscriptionDto.SubscriptionType,
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
        await cacheRepository.DeleteSubscriptionTypeAsync(subscription.SubscriptionType.Type);

        var remainingSubscriptions =
            await subscriptionRepository.GetSubscriptionsByEmailAsync(subscription.Subscriber!.Email);
        if (!remainingSubscriptions.Any())
        {
            Log.Information("Deleting subscriber with email: {Email}", subscription.Subscriber.Email);
            await subscriberRepository.DeleteSubscriberAsync(subscription.Subscriber);
            await cacheRepository.DeleteSubscriberAsync(subscription.Subscriber.Email);
        }
    }

    private async Task<SubscriptionType> GetSubscriptionTypeAsync(string subscriptionType)
    {
        var subscriptionTypeEntity = await cacheRepository.GetSubscriptionTypeByNameAsync(subscriptionType);
        if (subscriptionTypeEntity is not null)
        {
            return subscriptionTypeEntity;
        }

        subscriptionTypeEntity = await subscriptionTypeRepository.GetSubscriptionTypeByNameAsync(subscriptionType);
        if (subscriptionTypeEntity is null)
        {
            throw new NotFoundException("Subscription type not found");
        }

        await cacheRepository.IndexSubscriptionTypeAsync(subscriptionTypeEntity);

        return subscriptionTypeEntity;
    }

    private async Task<Subscriber> EnsureSubscriberExistsAsync(string email)
    {
        var subscriber = await cacheRepository.GetSubscriberByEmailAsync(email);
        if (subscriber is not null)
        {
            return subscriber;
        }

        subscriber = await subscriberRepository.GetSubscriberByEmailAsync(email);

        if (subscriber is null)
        {
            Log.Information("Creating new subscriber with email: {Email}", email);
            subscriber = new Subscriber
            {
                Id = Guid.NewGuid(),
                Email = email
            };
            await subscriberRepository.CreateSubscriberAsync(subscriber);
        }

        await cacheRepository.IndexSubscriberAsync(subscriber);
        return subscriber;
    }

    private async Task PublishMessage(SubscriptionCreatedEvent subscriptionCreatedEvent)
    {
        try
        {
            Log.Information(
                "Publishing SubscriptionCreatedEvent for email: {Email}, SubscriptionType: {SubscriptionType}",
                subscriptionCreatedEvent.Email, subscriptionCreatedEvent.SubscriptionType);
            await publishEndpoint.Publish(subscriptionCreatedEvent);
        }
        catch (Exception e)
        {
            Log.Error("Error publishing SubscriptionCreatedEvent: {Error}", e.Message);
        }
    }
}