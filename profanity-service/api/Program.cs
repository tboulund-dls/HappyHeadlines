using infrastrucure.Context;
using infrastrucure.implementations;
using infrastrucure.interfaces;
using service;
using service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IProfanityServiceArgs>(services =>
{
    var mongoDbContext = new MongoDbContext("mongodb://localhost:27017", "ProfanityService");
    var redisDbContext = new RedisDbContext("localhost:6379");
    var repository = new MongoDbRepository(mongoDbContext);
    var cache = new RedisDbRepository(redisDbContext, TimeSpan.FromHours(1));

    return new ProfanityServiceArgs
    {
        Repository = repository,
        CacheRepository = cache
    };
});

builder.Services.AddScoped<IService, ProfanityService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
