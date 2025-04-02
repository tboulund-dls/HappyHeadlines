using DraftService.Models;
using DraftService.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DraftService.Repository;

public class DraftRepository : IDraftRepository
{
    private readonly DraftContext _context;

    public DraftRepository(DraftContext context)
    {
        _context = context;
    }

    public async Task<DraftDto> GetDraftById(Guid id)
    {
        var result = await _context.Drafts
            .Where(d => d.Id == id)
            .Select(d => new DraftDto
                {
                    Id = d.Id,
                    Title = d.Title,
                    Content = d.Content,
                    AuthorId = d.AuthorId
                }
            ).FirstAsync();

        return result;
    }

    public async Task<List<DraftDto>> GetDraftByAuthorId(Guid authorId)
    {
        var result = await _context.Drafts
            .Where(d => d.AuthorId == authorId)
            .Select(d => new DraftDto
                {
                    Id = d.Id,
                    Title = d.Title,
                    Content = d.Content,
                    AuthorId = d.AuthorId
                }
            ).ToListAsync();

        return result;
    }

    public async Task<DraftDto> CreateDraft(CreateDraftDto draft)
    {
        var newDraft = new Draft
        {
            Id = Guid.NewGuid(),
            Title = draft.Title,
            Content = draft.Content,
            AuthorId = draft.AuthorId
        };

        _context.Drafts.Add(newDraft);
        await _context.SaveChangesAsync();

        var result = await _context.Drafts
            .Where(d => d.Id == newDraft.Id)
            .Select(u => new DraftDto
                {
                    Id = u.Id,
                    Title = u.Title,
                    Content = u.Content,
                    AuthorId = u.AuthorId
                }
            ).FirstAsync();
        
        return result;
    }

    public async Task UpdateDraft(DraftDto draft)
    {
        var draftToUpdate = await _context.Drafts.FirstAsync(d => d.Id == draft.Id);
        
        draftToUpdate.Title = draft.Title;
        draftToUpdate.Content = draft.Content;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteDraft(Guid id)
    {
        var draft = await _context.Drafts
            .FirstAsync(d => d.Id == id);

        _context.Drafts.Remove(draft);
        await _context.SaveChangesAsync();
    }
}