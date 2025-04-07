﻿using Microsoft.AspNetCore.Mvc;
using service.Interfaces;
using service.Models;

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
        IEnumerable<WordModel> result = await _service.getWords();
        
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
    public async Task<IActionResult> AddWord([FromBody] WordModel? word)
    {
        if (string.IsNullOrEmpty(word.Word)) return BadRequest();

        var response = await _service.AddWord(word);

        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteWord([FromBody] string word)
    {
        if (string.IsNullOrEmpty(word)) return BadRequest();
        
        bool success = await _service.DeleteWord(word);
        
        return success ? Ok() : Conflict();
    }
}