using System.ComponentModel.DataAnnotations;
using HappyHeadlines.Comments.Models.DTOs;
using HappyHeadlines.Comments.Services;
using Microsoft.AspNetCore.Mvc;

namespace HappyHeadlines.Comments.Controllers
{
    [ApiController]
    [Route("api/v1/comments")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("{articleId:guid}")]
        public async Task<ActionResult<CommentsResponseDTO>> GetCommentsByArticleId(
            Guid articleId,
            [FromQuery] [Range(1, int.MaxValue)] int page = 1,
            [FromQuery] [Range(1,50)] int pageSize = 20)
        {
            var response = await _commentService.GetCommentsByArticleIdAsync(articleId, page, pageSize);
            return Ok(response);
        }

        [HttpGet("comment/{commentId:guid}")]
        public async Task<ActionResult<CommentDTO>> GetCommentById(Guid commentId)
        {
            try
            {
                var comment = await _commentService.GetCommentByIdAsync(commentId);
                return Ok(comment);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost("{articleId:guid}")]
        public async Task<ActionResult<CommentDTO>> CreateComment(Guid articleId, [FromBody] NewCommentDTO commentDto)
        {
            if (articleId != commentDto.ArticleId)
            {
                return BadRequest("Article ID in URL must match the one in the request body");
            }
            
            var createdComment = await _commentService.CreateCommentAsync(commentDto);
            return CreatedAtAction(
                nameof(GetCommentById), 
                new { commentId = createdComment.Id }, 
                createdComment);
        }

        [HttpPut("{commentId:guid}")]
        public async Task<ActionResult<CommentDTO>> UpdateComment(Guid commentId, [FromBody] UpdateCommentDTO commentDto)
        {
            try
            {
                var updatedComment = await _commentService.UpdateCommentAsync(commentId, commentDto);
                return Ok(updatedComment);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{commentId:guid}")]
        public async Task<ActionResult> DeleteComment(Guid commentId)
        {
            try
            {
                await _commentService.DeleteCommentAsync(commentId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
