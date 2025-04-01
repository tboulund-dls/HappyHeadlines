using Microsoft.EntityFrameworkCore;
using SubscriberService.Models;

namespace SubscriberService.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public virtual DbSet<Subscriber> Subscribers { get; set; } = default!;
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}