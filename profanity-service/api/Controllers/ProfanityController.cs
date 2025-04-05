using Microsoft.AspNetCore.Mvc;
using service.Interfaces;
using SharedModels;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProfanityController : ControllerBase
{
    private readonly IService _service;

    public ProfanityController(IService service)
    {
        _service = service;
    }


    [HttpGet]
    public async Task<IActionResult> GetWords()
    {
        IEnumerable<string> result = await _service.getWords();
        
        return result.Any() ? Ok(result) : BadRequest();
    }
    
    [HttpPost]
    [Route("clean")]
    public async Task<IActionResult> Clean([FromBody] ProfanityFilterRequest message)
    {
        string text = message.Text;
        if (string.IsNullOrEmpty(text)) return BadRequest();

        string? clean = await _service.Clean(text);

        return !string.IsNullOrEmpty(clean) ? Ok(clean) : NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> AddWord([FromBody] string word)
    {
        if (string.IsNullOrEmpty(word)) return BadRequest();

        bool success = await _service.AddWord(word);

        return success ? Created() : Conflict();
    }

    [HttpDelete] 
    [Route("{wordId}")] 
    public async Task<IActionResult> DeleteWord([FromRoute] string wordId)
    {
        if (string.IsNullOrEmpty(wordId)) return BadRequest();
        
        bool success = await _service.DeleteWord(wordId);
        
        return success ? Ok() : Conflict();
    }
}