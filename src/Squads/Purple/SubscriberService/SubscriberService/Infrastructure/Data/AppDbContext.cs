using Microsoft.EntityFrameworkCore;
using SubscriberService.Models;

namespace SubscriberService.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public virtual DbSet<Subscriber> Subscribers { get; set; } = default!;
    public virtual DbSet<SubscriberType> SubscriberTypes { get; set; } = default!;
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
}