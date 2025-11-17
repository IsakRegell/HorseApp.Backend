namespace HorseApp.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string Email { get; set; } = string.Empty;

        public string DisplayName { get; set; } = string.Empty;

        public bool IsBankIdVerified { get; set; } //Meningen är BANK-ID-verifiering längre fram

        public DateTime? VerifiedAtUtc { get; set; }

        public Guid? LocationId { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime? UpdatedAtUtc { get; set; }
    }
}
