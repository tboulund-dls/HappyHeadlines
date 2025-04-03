using Microsoft.EntityFrameworkCore;
using SubscriberService.Domain.Models;
using SubscriberService.Infrastructure.Data;
using SubscriberService.Infrastructure.Repositories.Interfaces;

namespace SubscriberService.Infrastructure.Repositories.Implementation;

public class SubscriptionTypeRepository(AppDbContext dbContext) : ISubscriptionTypeRepository
{
    public async Task<SubscriptionType?> GetSubscriptionTypeByNameAsync(string name)
    {
        return await dbContext.SubscriberTypes
            .FirstOrDefaultAsync(st => st.Type == name);
    }
}