using HorseApp.Domain.Enums;

namespace HorseApp.Domain.Entities
{
    public class Post
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public User? User { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public PostType PostType { get; set; }

        public PostStatus Status { get; set; } = PostStatus.Active;

        public Guid LocationId { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime? UpdatedAtUtc { get; set; }

        public bool IsDeleted { get; set; } 

        public DateTime? DeletedAtUtc { get; set; }

        public ICollection<PostMedia> Media { get; set; } = new List<PostMedia>();
        public ICollection<PostLike> Likes { get; set; } = new List<PostLike>();
        public ICollection<PostFavorite> Favorites { get; set; } = new List<PostFavorite>();

    }
}
