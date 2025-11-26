namespace HorseApp.Application.DTOs.Users
{
    public class UserResponseDto
    {
        public Guid Id { get; set; }

        public string Username { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string DisplayName { get; set; } = default!;

        public int Age { get; set; }

        public string? Bio { get; set; }

        public string? ProfileImageUrl { get; set; }

        public bool IsBankIdVerified { get; set; }

        public DateTime? VerifiedAtUtc { get; set; }

        public Guid? LocationId { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime? UpdatedAtUtc { get; set; }
    }
}
