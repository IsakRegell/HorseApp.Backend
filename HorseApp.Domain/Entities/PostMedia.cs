namespace HorseApp.Domain.Entities
{
    public class PostMedia
    {
        public Guid Id { get; set; }

        public Guid PostId { get; set; }

        public Post? Post { get; set; }

        public string MediaUrl { get; set; } = string.Empty;

        public int SortOrder { get; set; } 

        public DateTime CreatedAtUtc { get; set; }
    }
}
