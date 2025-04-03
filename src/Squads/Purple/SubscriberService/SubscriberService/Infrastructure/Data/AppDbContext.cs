using Microsoft.EntityFrameworkCore;
using SubscriberService.Domain.Models;

namespace SubscriberService.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public virtual DbSet<Subscriber> Subscribers { get; set; } = default!;
    public virtual DbSet<SubscriptionType> SubscriberTypes { get; set; } = default!;
    public virtual DbSet<Subscription> Subscriptions { get; set; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // Get path from environment variable with fallback paths
        var dbPath = Environment.GetEnvironmentVariable("DB_PATH");
        
        if (string.IsNullOrEmpty(dbPath))
        {
            // For Docker environment
            if (Directory.Exists("/data"))
            {
                dbPath = "/data/Subscribers.db";
            }
            // For local development
            else
            {
                dbPath = "Subscribers.db"; // Local relative path
            }
        }
        
        options.UseSqlite($"Data Source={dbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SubscriptionType>()
            .HasData(new SubscriptionType
                {
                    Id = Guid.Parse("3cbad589-81b4-49c8-a48a-1cee835ea267"),
                    Type = "DAILY"
                },
                new SubscriptionType
                {
                    Id = Guid.Parse("9a24ad3e-4e3d-4f9b-953d-4c2b4f45abaa"),
                    Type = "NEWSSTREAM"
                });
        
        base.OnModelCreating(modelBuilder);
    }
}