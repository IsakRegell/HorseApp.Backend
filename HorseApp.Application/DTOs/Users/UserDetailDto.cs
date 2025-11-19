namespace HorseApp.Application.DTOs.Users
{
    public class UserDetailDto
    {
        public Guid Id { get; set; }

        public string UserName { get; set; } = default!;

        public string DisplayName { get; set; } = default!;

        public int age { get; set; }

        public string? ProfilePictureUrl { get; set; }

        public string? Bio { get; set; }

        public DateTime CreatedAtUtc { get; set; }
    }
}
