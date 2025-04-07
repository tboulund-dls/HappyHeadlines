using System;
using System.Threading.Tasks;
using AutoMapper;
using HappyHeadlines.Comments.Data;
using HappyHeadlines.Comments.Models;
using HappyHeadlines.Comments.Models.DTOs;

namespace HappyHeadlines.Comments.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _repository;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;

        public CommentService(ICommentRepository repository, IMapper mapper, HttpClient httpClient)
        {
            _repository = repository;
            _mapper = mapper;
            _httpClient = httpClient;
        }

        public async Task<CommentDTO> GetCommentByIdAsync(Guid id)
        {
            var comment = await _repository.GetCommentByIdAsync(id);
            if (comment == null)
            {
                throw new KeyNotFoundException($"Comment with ID {id} not found");
            }
            
            return _mapper.Map<CommentDTO>(comment);
        }

        public async Task<CommentsResponseDTO> GetCommentsByArticleIdAsync(Guid articleId, int page, int pageSize)
        {
            if (page < 1) page = 1;
            if (pageSize < 1 || pageSize > 100) pageSize = 20;

            var (comments, totalCount) = await _repository.GetCommentsByArticleIdAsync(articleId, page, pageSize);
            
            return new CommentsResponseDTO
            {
                Comments = _mapper.Map<System.Collections.Generic.List<CommentDTO>>(comments),
                TotalComments = totalCount,
                Page = page,
                PageSize = pageSize
            };
        }

        public async Task<CommentDTO> CreateCommentAsync(NewCommentDTO commentDto)
        {
            var cleanedContent = await TryCleanContentAsync(commentDto.Content);
            
            var comment = new Comment
            {
                ArticleId = commentDto.ArticleId,
                Content = cleanedContent,
                AuthorId = commentDto.AuthorId
            };

            var createdComment = await _repository.CreateCommentAsync(comment);
            return _mapper.Map<CommentDTO>(createdComment);
        }

        public async Task<CommentDTO> UpdateCommentAsync(Guid id, UpdateCommentDTO commentDto)
        {
            var existingComment = await _repository.GetCommentByIdAsync(id);
            if (existingComment == null)
            {
                throw new KeyNotFoundException($"Comment with ID {id} not found");
            }

            existingComment.Content = await TryCleanContentAsync(commentDto.Content);
            
            var updatedComment = await _repository.UpdateCommentAsync(existingComment);
            return _mapper.Map<CommentDTO>(updatedComment);
        }

        public async Task DeleteCommentAsync(Guid id)
        {
            var existingComment = await _repository.GetCommentByIdAsync(id);
            if (existingComment == null)
            {
                throw new KeyNotFoundException($"Comment with ID {id} not found");
            }

            await _repository.DeleteCommentAsync(id);
        }
        
        private async Task<string> TryCleanContentAsync(string originalContent)
        {
            try
            {
                var request = new
                {
                    Text = originalContent
                };

                var response = await _httpClient.PostAsJsonAsync("profanity/clean", request);

                if (response.IsSuccessStatusCode)
                {
                    var cleaned = await response.Content.ReadAsStringAsync();
                    return !string.IsNullOrWhiteSpace(cleaned) ? cleaned : originalContent;
                }

                return originalContent;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Profanity service failed: {ex.Message}");
                return originalContent; 
            }
        }
    }
}
