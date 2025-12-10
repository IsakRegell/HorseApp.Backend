using AutoMapper;
using HorseApp.Application.DTOs.Posts;
using HorseApp.Domain.Entities;

namespace HorseApp.Application.Mapping
{
    public class PostMappingProfile : Profile
    {
        public PostMappingProfile() 
        {
            CreateMap<CreatePostDto, Post>();
            CreateMap<Post, PostResponseDto>();
            CreateMap<Post, PostListItemDto>();

        }
    }
}
