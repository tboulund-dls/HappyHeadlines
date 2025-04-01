using MassTransit;
using Microsoft.EntityFrameworkCore;
using SubscriberService.Apis;
using SubscriberService.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Register DbContext
builder.Services.AddDbContext<AppDbContext>();

// Register MassTransit
builder.Services.AddMassTransit(options =>
{
    options.DisableUsageTelemetry();

    // Setup the Event Bus
    options.AddDelayedMessageScheduler();

    options.UsingRabbitMq((context, cfg) => {
        cfg.UseDelayedMessageScheduler();

        // Register host
        cfg.Host(Environment.GetEnvironmentVariable("RabbitMQ__Host"), "/", h =>
        {
            h.Username("RabbitMQ__Username");
            h.Password("RabbitMQ__Password");
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
