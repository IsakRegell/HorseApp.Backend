using HorseApp.Domain.Enums;

namespace HorseApp.Application.DTOs.Posts
{
    public class PostListItemDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public PostType PostType { get; set; }

        public PostStatus Status { get; set; }

        public DateTime CreatedAtUtc { get; set; }
    }
}
