using DraftService.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DraftService.Repository;

public interface IDraftRepository
{
    /**
     * Gets a draft by id.
     */
    Task<DraftDto> GetDraftById(Guid id);
    
    /**
     * Gets all drafts by author id.
     */
    Task<List<DraftDto>> GetDraftByAuthorId(Guid authorId);
    
    /**
     * Creates a new draft with given data.
     */
    Task<DraftDto> CreateDraft(CreateDraftDto draft);
    
    /**
     * Updates a draft with given data by id.
     */
    Task UpdateDraft(DraftDto draft);
    
    /**
     * Deletes a draft by id.
     */
    Task DeleteDraft(Guid id);
}