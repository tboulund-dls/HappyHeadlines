using System.Collections.Concurrent;
using article_publisher.api.Models;
using article_publisher.api.Services;
using Microsoft.Net.Http.Headers;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton(new ConcurrentBag<Article>());
builder.Services.AddSingleton(new ConcurrentBag<Draft>());
builder.Services.AddSingleton<ArticleQueueService>();
builder.Services.AddSingleton<PublisherService>();
builder.Services.AddSingleton<IConnection>(sp =>
{
    var factory = sp.GetRequiredService<IConnectionFactory>();
    return factory.CreateConnection();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var frontEndRelativePath = "frontend/www/";

builder.Services.AddSpaStaticFiles(configuration => 
    { configuration.RootPath = frontEndRelativePath; });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder
            .WithOrigins("https://trustedwebsite.com", "https://anothertrustedwebsite.com") 
            .AllowAnyMethod()
            .AllowAnyHeader());
});



var app = builder.Build();
app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(options =>
{
    options.SetIsOriginAllowed(origin => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
});

app.UseSpaStaticFiles(new StaticFileOptions()
{
    OnPrepareResponse = ctx =>
    {
        const int durationInSeconds = 60 * 60 * 24;
        ctx.Context.Response.Headers[HeaderNames.CacheControl] =
            "public,max-age=" + durationInSeconds;
    }
});

app.Map("/frontend",
    (IApplicationBuilder frontendApp) =>
    { frontendApp.UseSpa(spa =>
        { spa.Options.SourcePath = "frontend/www/"; }); });


app.UseSpa(conf =>
{
    conf.Options.SourcePath = frontEndRelativePath;
});


app.MapControllers();
app.Run();