using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace newsletterservice.Services
{
public class NewsletterService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<NewsletterService> _logger;

    public NewsletterService(HttpClient httpClient, ILogger<NewsletterService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<object> GetArticlesFromArticleServiceAsync()
    {
        _logger.LogInformation("Fetching articles from ArticleService");

        await Task.CompletedTask;

        return new { message = "Placeholder for articles" };
    }
}
}