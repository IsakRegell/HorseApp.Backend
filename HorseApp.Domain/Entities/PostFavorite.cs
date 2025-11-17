namespace HorseApp.Domain.Entities
{
    public class PostFavorite
    {
        public Guid Id { get; set; }

        public Guid PostId { get; set; }

        public Guid UserId { get; set; }

        public DateTime CreatedAt { get; set; } 
    }
}
