namespace DraftService.Models;

public class Draft
{
    /**
     * ID of the draft.
     */
    public Guid Id { get; set; }
    
    /**
     * Title of the draft.
     */
    public required string Title { get; set; }
    
    /**
     * Content of the draft.
     */
    public string? Content { get; set; }
    
    /**
     * ID of the author.
     */
    public Guid AuthorId { get; set; }
}