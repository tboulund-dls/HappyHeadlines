using infrastrucure.Context;
using infrastrucure.implementations;
using infrastrucure.interfaces;
using service;
using service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IProfanityServiceArgs>(services =>
{
    var mongoDbContext = new MongoDbContext( Environment.GetEnvironmentVariable("mongoconn"), Environment.GetEnvironmentVariable("dbname"));
    var redisDbContext = new RedisDbContext(Environment.GetEnvironmentVariable("redisconn"));
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

app.UseCors(policy =>
    policy
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
