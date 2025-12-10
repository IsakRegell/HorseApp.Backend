namespace HorseApp.Domain.Entities
{
    public class PostFavorite
    {
        public Guid Id { get; set; }

        public Guid PostId { get; set; }

        public Post? Post { get; set; }

        public Guid UserId { get; set; }

        public User? User { get; set; }

        public DateTime CreatedAtUtc { get; set; } 
    }
}
