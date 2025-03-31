namespace article_publisher.api.Models;

public class Draft
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; }
    public string Content { get; set; }
    public Author Author { get; set; }
}