using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HappyHeadlines.Comments.Models;

namespace HappyHeadlines.Comments.Data
{
    public interface ICommentRepository
    {
        Task<Comment> GetCommentByIdAsync(Guid id);
        Task<(List<Comment> comments, int totalCount)> GetCommentsByArticleIdAsync(Guid articleId, int page, int pageSize);
        Task<Comment> CreateCommentAsync(Comment comment);
        Task<Comment> UpdateCommentAsync(Comment comment);
        Task DeleteCommentAsync(Guid id);
    }
}
