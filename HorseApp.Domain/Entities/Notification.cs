using HorseApp.Domain.Enums;

namespace HorseApp.Domain.Entities
{
    public class Notification
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public NotificationType Type { get; set; }

        public Guid? PostId { get; set; }

        public Guid? ConversationId { get; set; }

        public Guid? MessageId { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public bool IsRead { get; set; }
    }
}
