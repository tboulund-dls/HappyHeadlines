using article_publisher.api.Models;
using article_publisher.api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton(new List<Article>());
builder.Services.AddSingleton(new List<Draft>());
builder.Services.AddSingleton<ArticleQueueService>();
builder.Services.AddSingleton<PublisherService>();

var app = builder.Build();

app.UseRouting();
app.MapControllers();
app.Run();