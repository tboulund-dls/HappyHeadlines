using article_publisher.api.Models;

public class Author
{
    public string Name { get; set; }
    public string Email { get; set; }
    public List<Article> Articles { get; set; } = new();
    public List<Comment> Comments { get; set; } = new();
    
}