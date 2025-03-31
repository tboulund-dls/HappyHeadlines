using article_publisher.api.Models;
using article_publisher.api.Services;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton(new List<Article>());
builder.Services.AddSingleton(new List<Draft>());
builder.Services.AddSingleton<ArticleQueueService>();
builder.Services.AddSingleton<PublisherService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var frontEndRelativePath = "./../frontend/www/";

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
        { spa.Options.SourcePath = "./../frontend/www/"; }); });


app.UseSpa(conf =>
{
    conf.Options.SourcePath = frontEndRelativePath;
});


app.UseRouting();
app.MapControllers();
app.Run();