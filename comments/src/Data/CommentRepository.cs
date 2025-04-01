using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HappyHeadlines.Comments.Models;
using Microsoft.EntityFrameworkCore;

namespace HappyHeadlines.Comments.Data
{
    public class CommentRepository : ICommentRepository
    {
        private readonly CommentDbContext _context;

        public CommentRepository(CommentDbContext context)
        {
            _context = context;
        }

        public async Task<Comment> GetCommentByIdAsync(Guid id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task<(List<Comment> comments, int totalCount)> GetCommentsByArticleIdAsync(Guid articleId, int page, int pageSize)
        {
            var query = _context.Comments
                .Where(c => c.ArticleId == articleId)
                .OrderByDescending(c => c.CreatedAt);

            var totalCount = await query.CountAsync();
            var comments = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (comments, totalCount);
        }

        public async Task<Comment> CreateCommentAsync(Comment comment)
        {
            comment.CreatedAt = DateTime.UtcNow;
            comment.UpdatedAt = DateTime.UtcNow;
            
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            
            return comment;
        }

        public async Task<Comment> UpdateCommentAsync(Comment comment)
        {
            comment.UpdatedAt = DateTime.UtcNow;
            
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
            
            return comment;
        }

        public async Task DeleteCommentAsync(Guid id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
