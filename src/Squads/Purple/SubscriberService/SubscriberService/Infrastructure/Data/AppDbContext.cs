using Microsoft.EntityFrameworkCore;
using SubscriberService.Models;

namespace SubscriberService.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public virtual DbSet<Subscriber> Subscribers { get; set; } = default!;
    public virtual DbSet<SubscriberType> SubscriberTypes { get; set; } = default!;
    public virtual DbSet<Subscription> Subscriptions { get; set; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source=Subscribers.db");
}