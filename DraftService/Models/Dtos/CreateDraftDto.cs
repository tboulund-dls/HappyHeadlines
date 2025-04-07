namespace DraftService.Models.Dtos;

public class CreateDraftDto
{
    public required string Title { get; set; }
    public string? Content { get; set; }
    
    public Guid AuthorId { get; set; }
}