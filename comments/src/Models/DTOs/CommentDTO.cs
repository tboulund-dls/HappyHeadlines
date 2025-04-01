using System;

namespace HappyHeadlines.Comments.Models.DTOs
{
    public class CommentDTO
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Guid ArticleId { get; set; }
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
