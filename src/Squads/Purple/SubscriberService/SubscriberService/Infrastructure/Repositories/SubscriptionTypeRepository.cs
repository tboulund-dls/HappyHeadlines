using Microsoft.EntityFrameworkCore;
using SubscriberService.Infrastructure.Data;
using SubscriberService.Models;

namespace SubscriberService.Infrastructure.Repositories;

public class SubscriptionTypeRepository(AppDbContext dbContext) : ISubscriptionTypeRepository
{
    public async Task<SubscriberType?> GetSubscriptionTypeByNameAsync(string name)
    {
        return await dbContext.SubscriberTypes
            .FirstOrDefaultAsync(st => st.Type == name);
    }
}