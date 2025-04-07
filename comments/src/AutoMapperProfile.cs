using AutoMapper;
using HappyHeadlines.Comments.Models;
using HappyHeadlines.Comments.Models.DTOs;

namespace HappyHeadlines.Comments
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Comment, CommentDTO>();
            CreateMap<NewCommentDTO, Comment>();
            CreateMap<UpdateCommentDTO, Comment>();
        }
    }
}
