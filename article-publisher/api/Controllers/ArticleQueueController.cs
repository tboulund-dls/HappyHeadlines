using article_publisher.api.Services;
using Microsoft.AspNetCore.Mvc;

namespace article_publisher.api.Controllers;

/// <summary>
/// testing purpose
/// </summary>
[ApiController]
[Route("queue")]
public class ArticleQueueController : ControllerBase
{
    private readonly ArticleQueueService _queue;

    public ArticleQueueController(ArticleQueueService queue)
    {
        _queue = queue;
    }

    [HttpGet]
    public IActionResult GetQueue()
    {
        return Ok(_queue.GetAll());
    }
}