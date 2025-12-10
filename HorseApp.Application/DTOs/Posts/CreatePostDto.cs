using HorseApp.Domain.Enums;

namespace HorseApp.Application.DTOs.Posts
{
    public class CreatePostDto
    {
        public Guid UserId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty; 

        public PostType PostType { get; set; }

        public Guid LocationId { get; set; }
    }
}
