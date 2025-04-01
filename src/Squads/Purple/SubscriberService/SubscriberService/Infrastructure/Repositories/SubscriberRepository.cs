using Microsoft.EntityFrameworkCore;
using SubscriberService.Infrastructure.Data;
using SubscriberService.Models;

namespace SubscriberService.Infrastructure.Repositories;

public class SubscriberRepository(AppDbContext dbContext) : ISubscriberRepository
{
    public async Task<IEnumerable<Subscriber>> GetSubscribersForSubscriptionTypeAsync(SubscriberType subscriberType)
    {
        return await dbContext.Subscriptions
            .Include(s => s.Subscriber)
            .Include(t => t.Type)
            .Where(s => s.TypeId == subscriberType.Id)
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
    
    public async Task<Subscriber?> GetSubscriberByIdAsync(Guid subscriberId)
    {
        return await dbContext.Subscribers
            .FirstOrDefaultAsync(s => s.Id == subscriberId);
    }
    
    public async Task DeleteSubscriberAsync(Guid subscriberId)
    {
        var subscriber = await GetSubscriberByIdAsync(subscriberId);
        if (subscriber != null)
        {
            dbContext.Subscribers.Remove(subscriber);
            await dbContext.SaveChangesAsync();
        }
    }
}