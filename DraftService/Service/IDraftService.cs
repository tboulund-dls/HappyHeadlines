using DraftService.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DraftService.Service;

public interface IDraftService
{
    /**
     * Gets a draft by id.
     */
    DraftDto GetDraft(int id);
    
    /**
     * Creates a new draft with given data.
     */
    DraftDto CreateDraft(CreateDraftDto draft);
}