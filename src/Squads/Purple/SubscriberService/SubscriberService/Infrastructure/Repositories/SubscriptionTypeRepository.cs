using Microsoft.EntityFrameworkCore;
using SubscriberService.Domain.Models;
using SubscriberService.Infrastructure.Data;

namespace SubscriberService.Infrastructure.Repositories;

public class SubscriptionTypeRepository(AppDbContext dbContext) : ISubscriptionTypeRepository
{
    public async Task<SubscriptionType?> GetSubscriptionTypeByNameAsync(string name)
    {
        return await dbContext.SubscriberTypes
            .FirstOrDefaultAsync(st => st.Type == name);
    }
}