using Microsoft.EntityFrameworkCore;
using SubscriberService.Domain.Models;
using SubscriberService.Infrastructure.Data;

namespace SubscriberService.Infrastructure.Repositories;

public class SubscriberRepository(AppDbContext dbContext) : ISubscriberRepository
{
    public async Task<IEnumerable<Subscriber>> GetSubscribersForSubscriptionTypeAsync(SubscriptionType subscriptionType)
    {
        return await dbContext.Subscriptions
            .Include(s => s.Subscriber)
            .Include(t => t.SubscriptionType)
            .Where(s => s.SubscriptionTypeId == subscriptionType.Id)
            .Select(s => s.Subscriber!)
            .ToListAsync();
    }

    public async Task<Subscriber> CreateSubscriberAsync(Subscriber subscriber)
    {
        var result = await dbContext.Subscribers.AddAsync(subscriber);
        await dbContext.SaveChangesAsync();
        return result.Entity;
    }
    
    public async Task<Subscriber?> GetSubscriberByEmailAsync(string email)
    {
        return await dbContext.Subscribers
            .FirstOrDefaultAsync(s => s.Email == email);
    }

    public async Task DeleteSubscriberAsync(Subscriber subscriber)
    {
        dbContext.Subscribers.Remove(subscriber);
        await dbContext.SaveChangesAsync();
    }
}