namespace article_publisher.api.Models;

public class Comment
{
    public string Content { get; set; }
    public Author Author { get; set; }
    public DateTime PostedAt { get; set; }
}