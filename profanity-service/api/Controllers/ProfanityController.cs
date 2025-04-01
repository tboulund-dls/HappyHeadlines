using Microsoft.AspNetCore.Mvc;
using service.Interfaces;

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
    
    [HttpGet]
    [Route("clean")]
    public async Task<IActionResult> Clean([FromBody] string message)
    {
        if (string.IsNullOrEmpty(message)) return BadRequest();

        string? clean = await _service.Clean(message);

        return !string.IsNullOrEmpty(clean) ? Ok(clean) : NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> AddWord([FromBody] string? word)
    {
        if (string.IsNullOrEmpty(word)) return BadRequest();

        bool success = await _service.AddWord(word);

        return success ? Created() : Conflict();
    }

    [HttpDelete]
    [Route("{wordId}")]
    public async Task<IActionResult> DeleteWord([FromRoute] int wordId)
    {
        if (wordId <= 0) return BadRequest();
        
        bool success = await _service.DeleteWord(wordId);
        
        return success ? Ok() : Conflict();
    }
}