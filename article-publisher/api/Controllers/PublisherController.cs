using article_publisher.api.Models;
using article_publisher.api.Services;
using Microsoft.AspNetCore.Mvc;

namespace article_publisher.api.Controllers;

[ApiController]
[Route("")]
public class PublisherController : ControllerBase
{
    private readonly PublisherService _publisher;

    public PublisherController(PublisherService publisher)
    {
        _publisher = publisher;
    }

    [HttpPost("draft")]
    public IActionResult SaveDraft([FromBody] Draft draft)
    {
        _publisher.SaveDraft(draft);
        return Ok(draft);
    }

    [HttpPost("publish")]
    public IActionResult Publish([FromBody] Draft draft)
    {
        var article = _publisher.PublishDraft(draft);
        return Ok(article);
    }

    [HttpGet("status/{articleId}")]
    public IActionResult GetStatus(string articleId)
    {
        var status = _publisher.GetStatus(articleId);
        return Ok(new { status });
    }

    [HttpGet("published")]
    public IActionResult GetPublished() => Ok(_publisher.GetPublishedArticles());
    

    [HttpGet("drafts")]
    public IActionResult GetDrafts() => Ok(_publisher.GetDrafts());
    
    
    
    [HttpDelete("article/{articleId}")]
    public IActionResult DeleteArticle(string articleId)
    {
        var result = _publisher.DeleteArticle(articleId);
        if (!result) return NotFound(new { message = "Article not found." });

        return Ok(new { message = "Article deleted successfully." });
    }
    

    [HttpPut("article/{articleId}")]
    public IActionResult UpdateArticle(string articleId, [FromBody] Article updatedArticle)
    {
        var result = _publisher.UpdateArticle(articleId, updatedArticle);
        if (result == null) return NotFound(new { message = "Article not found." });

        return Ok(result);
    }
    
    [HttpDelete("draft/{draftId}")]
    public IActionResult DeleteDraft(string draftId)
    {
        var result = _publisher.DeleteDraft(draftId);
        if (!result) return NotFound(new { message = "Draft not found." });

        return Ok(new { message = "Draft deleted successfully." });
    }

    [HttpPut("draft/{draftId}")]
    public IActionResult UpdateDraft(string draftId, [FromBody] Draft updatedDraft)
    {
        var result = _publisher.UpdateDraft(draftId, updatedDraft);
        if (result == null) return NotFound(new { message = "Draft not found." });

        return Ok(result);
    }

}