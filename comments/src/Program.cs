using HappyHeadlines.Comments.Data;
using HappyHeadlines.Comments.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Build connection string from environment variables to match docker-compose settings
var dbUser = Environment.GetEnvironmentVariable("DB_USER") ?? "postgres";
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "postgres";
var dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "comments";
var dbPort = Environment.GetEnvironmentVariable("DB_PORT") ?? "5432";
var dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? 
    (builder.Environment.IsDevelopment() ? "localhost" : "comments-db");

var connectionString = $"Host={dbHost};Port={dbPort};Database={dbName};Username={dbUser};Password={dbPassword}";

builder.Services.AddDbContext<CommentDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddHttpClient<ICommentService, CommentService>(client =>
{
    client.BaseAddress = new Uri("http://profanityapi:9000/");
});

// Add automapper
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
