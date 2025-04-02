using DraftService.Models.Dtos;
using DraftService.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DraftService.Controller;

[Route("[controller]")]
[ApiController]
public class DraftController : ControllerBase
{
    private readonly IDraftRepository _draftRepository;

    public DraftController(IDraftRepository draftService)
    {
        _draftRepository = draftService;
    }
    
    /**
     * Gets a draft by id.
     */
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DraftDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DraftDto>> GetDraftById(Guid id)
    {
        return Ok(await _draftRepository.GetDraftById(id));
    }

    /**
     * Gets all drafts by author id.
     */
    [HttpGet("author/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DraftDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<DraftDto>>> GetDraftByAuthorId(Guid authorId)
    {
        return Ok(await _draftRepository.GetDraftByAuthorId(authorId));
    }
    
    /**
     * Creates a new draft with given data.
     */
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult CreateDraft([FromBody] CreateDraftDto draft)
    {
        try
        {
            var result = _draftRepository.CreateDraft(draft);

            return new CreatedResult($"/draft/{result.Id}", result);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    /**
     * Updates a draft by id.
     */
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> UpdateDraft(DraftDto draft)
    {
        if (draft.Id == Guid.Empty)
        {
            return BadRequest();
        }

        await _draftRepository.UpdateDraft(draft);
        
        return Ok();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteDraft(Guid id)
    {
        _draftRepository.DeleteDraft(id);
        return NoContent();
    }
}