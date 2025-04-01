using MassTransit;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using SubscriberService.Apis;
using SubscriberService.Application.Services;
using SubscriberService.Infrastructure.Data;
using SubscriberService.Infrastructure.Repositories;
using SubscriberService.Infrastructure.Repositories.Implementation;
using SubscriberService.Infrastructure.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// DevOps
// - Fault-isolated design ✅
// - Relevant logging ✅
// - Good planning and documentation ✅
// - Avoid extra processing ✅

// Green initiatives
// - Reduce network package size ✅
// - Cache data closer to the user ✅
// - SQLite ✅

builder.Logging.ClearProviders();
Log.Logger = new LoggerConfiguration()
    // Set the default minimum level as Debug
    .MinimumLevel.Is(LogEventLevel.Debug)
    // Override specific categories:
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Warning)
    // Write logs to console with a custom output template
    .WriteTo.Console(
        outputTemplate:
        "{Timestamp:yyyy-MM-dd HH:mm:ss}|{MachineName}|{ThreadId}|{RequestId}|{Level:u3}|{Message:lj}{NewLine}{Exception}")
    .CreateLogger();

builder.Logging.AddSerilog();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Register DbContext
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddSingleton<RedisContext>(provider =>
{
    var redisConnectionString = builder.Configuration["Redis:ConnectionString"] ?? "localhost:6379";
    return new RedisContext(redisConnectionString);
});

builder.Services.AddScoped<ISubscriberRepository, SubscriberRepository>();
builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
builder.Services.AddScoped<ISubscriptionTypeRepository, SubscriptionTypeRepository>();
builder.Services.AddScoped<ISubscriberService, SubscriberService.Application.Services.SubscriberService>();
builder.Services.AddScoped<ICacheRepository, RedisCacheRepository>(provider =>
{
    var redisContext = provider.GetService<RedisContext>();
    var timeToLive = TimeSpan.FromMinutes(5);
    return new RedisCacheRepository(redisContext, timeToLive);
});

// Register MassTransit
builder.Services.AddMassTransit(options =>
{
    options.DisableUsageTelemetry();

    // Setup the Event Bus
    options.AddDelayedMessageScheduler();

    options.UsingRabbitMq((context, cfg) => {
        cfg.UseDelayedMessageScheduler();

        // Register host
        cfg.Host(builder.Configuration["RabbitMQ:Host"] ?? "localhost", "/", h =>
        {
            h.Username(builder.Configuration["RabbitMQ:Username"] ?? "guest");
            h.Password(builder.Configuration["RabbitMQ:Password"] ?? "guest");
        });

        // Retry an additional three times after 5, 10, and 15 minutes
        cfg.UseDelayedRedelivery(r => r.Intervals(
            TimeSpan.FromMinutes(5),
            TimeSpan.FromMinutes(10),
            TimeSpan.FromMinutes(15)));
        // 2 immediate in memory retries
        cfg.UseMessageRetry(r => r.Immediate(2));

        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    
    var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

// Apply migrations automatically at startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

app.UseHttpsRedirection();

app.AddSubscriberApi();

app.Run();
