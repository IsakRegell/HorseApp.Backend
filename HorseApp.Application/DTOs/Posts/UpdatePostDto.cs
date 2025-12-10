using HorseApp.Domain.Enums;

namespace HorseApp.Application.DTOs.Posts
{
    public class UpdatePostDto
    {
        public string? Title { get; set; }

        public string? Description { get; set; }
        
        public PostStatus? Status { get; set; }

        public Guid? LocationId { get; set; }

        public DateTime? UpdatedAtUtc { get; set; }
    }
}
