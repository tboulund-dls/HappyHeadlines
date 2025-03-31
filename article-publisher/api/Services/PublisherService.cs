using article_publisher.api.Models;

namespace article_publisher.api.Services;

public class PublisherService
{
    private readonly List<Article> _articles;
    private readonly List<Draft> _drafts;
    private readonly ArticleQueueService _articleQueueService;

    public PublisherService(List<Article> articles, List<Draft> drafts, ArticleQueueService articleQueueService)
    {
        _articles = articles;
        _drafts = drafts;
        _articleQueueService = articleQueueService;
    }

    //  saves a draft(not publish)
    public void SaveDraft(Draft draft)
    {
        _drafts.Add(draft);
    }

     // converts a draft to an article
    public Article PublishDraft(Draft draft)
    {
        _drafts.RemoveAll(d => d.Id == draft.Id); //remove the draft from the list of drafts

        var article = new Article   //create a new article
        { 
            Id = Guid.NewGuid().ToString(),
            Title = draft.Title,
            Content = draft.Content,
            Author = draft.Author,
            PublishedAt = DateTime.UtcNow
        };

        _articles.Add(article); //add the article to the list of published articles
        _articleQueueService.Enqueue(article); //add the article to the queue
        return article;
    }
    public bool DeleteDraft(string draftId)
    {
        var draft = _drafts.FirstOrDefault(d => d.Id == draftId);
        if (draft == null) return false;

        _drafts.Remove(draft);
        return true;
    }

    public Draft? UpdateDraft(string draftId, Draft updatedDraft)
    {
        var existing = _drafts.FirstOrDefault(d => d.Id == draftId);
        if (existing == null) return null;

        existing.Title = updatedDraft.Title;
        existing.Content = updatedDraft.Content;
        existing.Author = updatedDraft.Author;

        return existing;
    }

    //  returns all published articles
    public List<Article> GetPublishedArticles() => _articles; 
    
    //  returns all drafts
    public List<Draft> GetDrafts() => _drafts;

    
    // tells if an article is "published" or "not found"
    public string GetStatus(string articleId)
    {
        return _articles.Any(a => a.Id == articleId) ? "Published" : "Not Found";
    }
    
    
    public bool DeleteArticle(string articleId)
    {
        var article = _articles.FirstOrDefault(a => a.Id == articleId);
        if (article == null) return false;

        _articles.Remove(article);
        return true;
    }

    public Article? UpdateArticle(string articleId, Article updatedArticle)
    {
        var existing = _articles.FirstOrDefault(a => a.Id == articleId);
        if (existing == null) return null;

        existing.Title = updatedArticle.Title;
        existing.Content = updatedArticle.Content;
        existing.Author = updatedArticle.Author;
        existing.PublishedAt = DateTime.UtcNow;

        return existing;
    }

}