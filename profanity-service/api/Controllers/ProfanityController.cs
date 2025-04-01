using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProfanityController : ControllerBase
{


    [HttpGet]
    public async Task<IActionResult> GetWords()
    {
        return BadRequest();
    }
    
    [HttpGet]
    public async Task<IActionResult> Clean([FromBody] string message)
    {
        if (string.IsNullOrEmpty(message)) return BadRequest();
        
        return BadRequest();
    }

    [HttpPost]
    public async Task<IActionResult> AddWord([FromBody] string? word)
    {
        if (string.IsNullOrEmpty(word)) return BadRequest();
        
        return BadRequest();
    }

    [HttpDelete]
    [Route("{wordId}")]
    public async Task<IActionResult> DeleteWord([FromRoute] int wordId)
    {
        if (wordId <= 0) return BadRequest();
        
        return BadRequest();
    }
}