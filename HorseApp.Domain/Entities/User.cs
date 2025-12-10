namespace HorseApp.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string DisplayName { get; set; } = string.Empty;

        public int Age { get; set; }

        public string? Bio { get; set; }

        public string? ProfileImageUrl { get; set; }

        public bool IsBankIdVerified { get; set; } //Meningen är BANK-ID-verifiering längre fram

        public DateTime? VerifiedAtUtc { get; set; }

        public Guid? LocationId { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime? UpdatedAtUtc { get; set; }

        public bool IsDeleted { get; set; }          // markerar om användaren är soft-deleted

        public DateTime? DeletedAtUtc { get; set; }  // när användaren soft-deletades

    }
}
