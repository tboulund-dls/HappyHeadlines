using Microsoft.EntityFrameworkCore;
using SubscriberService.Infrastructure.Data;
using SubscriberService.Models;

namespace SubscriberService.Infrastructure.Repositories;

public class SubscriptionRepository(AppDbContext dbContext) : ISubscriptionRepository
{
    public async Task<IEnumerable<Subscription>> GetSubscriptionsByEmailAsync(string email)
    {
        var subscriptions = await dbContext.Subscriptions
            .Include(s => s.Subscriber)
            .Where(s => s.Subscriber!.Email == email)
            .ToListAsync();
        
        return subscriptions;
    }

    public async Task<Subscription?> GetSubscriptionByIdAsync(Guid subscriptionId)
    {
        return await dbContext.Subscriptions
            .Include(s => s.Subscriber)
            .Include(t => t.SubscriptionType)
            .FirstOrDefaultAsync(s => s.Id == subscriptionId);
    }

    public async Task<Subscription> SubscribeAsync(Subscription subscription)
    {
        var result = await dbContext.Subscriptions.AddAsync(subscription);
        await dbContext.SaveChangesAsync();
        return result.Entity;
    }

    public async Task UnsubscribeAsync(Subscription subscription)
    {
        dbContext.Subscriptions.Remove(subscription);
        await dbContext.SaveChangesAsync();
    }
}