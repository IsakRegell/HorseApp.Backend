namespace HorseApp.Domain.Entities
{
    public class Conversation
    {
        public Guid Id { get; set; }

        public Guid PostId { get; set; }

        public Guid OwnerUserId { get; set; }

        public Guid StarterUserId { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime? LastMessageAtUtc { get; set; }
    }
}
