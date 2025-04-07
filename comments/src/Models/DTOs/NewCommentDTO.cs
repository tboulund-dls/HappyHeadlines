using System;
using System.ComponentModel.DataAnnotations;

namespace HappyHeadlines.Comments.Models.DTOs
{
    public class NewCommentDTO
    {
        [Required]
        public Guid ArticleId { get; set; }
        [Required]
        public string Content { get; set; } = string.Empty;
        [Required]
        public Guid AuthorId { get; set; }
    }
}
