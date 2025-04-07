using System;
using System.Threading.Tasks;
using HappyHeadlines.Comments.Models.DTOs;

namespace HappyHeadlines.Comments.Services;

public interface ICommentService
{
	Task<CommentDTO> GetCommentByIdAsync(Guid id);
    Task<CommentsResponseDTO> GetCommentsByArticleIdAsync(Guid articleId, int page, int pageSize);
    Task<CommentDTO> CreateCommentAsync(NewCommentDTO commentDto);
    Task<CommentDTO> UpdateCommentAsync(Guid id, UpdateCommentDTO commentDto);
    Task DeleteCommentAsync(Guid id);
	Task<string> CleanTextAsync(string text);
}

