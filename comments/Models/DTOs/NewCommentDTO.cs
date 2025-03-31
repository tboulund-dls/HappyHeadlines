using System;

namespace HappyHeadlines.Comments.Models.DTOs
{
    public class NewCommentDTO
    {
        public Guid ArticleId { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
    }
}
