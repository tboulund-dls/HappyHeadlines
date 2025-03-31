using article_publisher.api.Models;

public class Article
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; }
    public string Content { get; set; }
    public Author Author { get; set; }
    public DateTime PublishedAt { get; set; }
    public List<Comment> Comments { get; set; } = new();
}