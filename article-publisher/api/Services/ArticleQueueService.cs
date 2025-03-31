namespace article_publisher.api.Services;

/// <summary>
/// for testing....need to be changed with RabbitMQ
/// </summary>
public class ArticleQueueService
{
    private readonly Queue<Article> _queue = new();

    // adds a published article to the queeue
    public void Enqueue(Article article) => _queue.Enqueue(article);

    // returns the current list in the queue
    public List<Article> GetAll() => _queue.ToList();
}
