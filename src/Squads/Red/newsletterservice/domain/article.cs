namespace newsletterservice.Models
{
    public class Article
{
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public DateTime PostedAt { get; set; }
}
}