using DraftService.Models;
using DraftService.Models.Dtos;
using DraftService.Service;
using Microsoft.AspNetCore.Mvc;

namespace DraftService.Controller;

[Route("[controller]")]
[ApiController]
public class DraftController : ControllerBase
{
    private readonly IDraftService _draftService;

    public DraftController(IDraftService draftService)
    {
        _draftService = draftService;
    }
    
    /**
     * Gets a draft by id.
     */
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DraftDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DraftDto> Get(int id)
    {
        return Ok(_draftService.GetDraft(id));
    }

    /**
     * Creates a new draft with given data.
     */
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult CreateDraft(CreateDraftDto draft)
    {
        try
        {
            var result = _draftService.CreateDraft(draft);
            
            return new CreatedResult($"/draft/{result.Id}", result);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}