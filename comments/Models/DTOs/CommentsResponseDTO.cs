using System.Collections.Generic;

namespace HappyHeadlines.Comments.Models.DTOs
{
    public class CommentsResponseDTO
    {
        public List<CommentDTO> Comments { get; set; }
        public int TotalComments { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
