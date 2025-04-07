using System.ComponentModel.DataAnnotations;

namespace HappyHeadlines.Comments.Models.DTOs
{
    public class UpdateCommentDTO
    {
        [Required]
        public string Content { get; set; } = string.Empty;
    }
}
