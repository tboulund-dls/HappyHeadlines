using System.Text.Json;
using SubscriberService.Domain.Models;
using SubscriberService.Infrastructure.Data;
using SubscriberService.Infrastructure.Repositories.Interfaces;

namespace SubscriberService.Infrastructure.Repositories.Implementation;

public class RedisCacheRepository(RedisContext context, TimeSpan timeToLive) : ICacheRepository
{
    public async Task<Subscriber?> GetSubscriberByEmailAsync(string email)
    {
        var key = $"subscriber:{email}";
        var subscriber = await context.Database.StringGetAsync(key);
        return subscriber.IsNullOrEmpty ? null : JsonSerializer.Deserialize<Subscriber>(subscriber);
    }

    public async Task IndexSubscriberAsync(Subscriber subscriber)
    {
        var key = $"subscriber:{subscriber.Email}";
        var subscriberJson = JsonSerializer.Serialize(subscriber);
        await context.Database.StringSetAsync(key, subscriberJson, timeToLive);
    }

    public async Task DeleteSubscriberAsync(string email)
    {
        var key = $"subscriber:{email}";
        await context.Database.KeyDeleteAsync(key);
    }

    public async Task<SubscriptionType?> GetSubscriptionTypeByNameAsync(string name)
    {
        var key = $"subscription_type:{name}";
        var subscriptionType = await context.Database.StringGetAsync(key);
        return subscriptionType.IsNullOrEmpty ? null : JsonSerializer.Deserialize<SubscriptionType>(subscriptionType);
    }

    public async Task IndexSubscriptionTypeAsync(SubscriptionType subscriptionType)
    {
        var key = $"subscription_type:{subscriptionType.Type}";
        var subscriptionTypeJson = JsonSerializer.Serialize(subscriptionType);
        await context.Database.StringSetAsync(key, subscriptionTypeJson, timeToLive);
    }

    public async Task DeleteSubscriptionTypeAsync(string name)
    {
        var key = $"subscription_type:{name}";
        await context.Database.KeyDeleteAsync(key);
    }
}