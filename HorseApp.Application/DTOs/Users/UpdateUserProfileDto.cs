namespace HorseApp.Application.DTOs.Users
{
    public class UpdateUserProfileDto
    {
        public string DisplayName { get; set; } = default!;

        public int Age { get; set; }

        public string? ProfilePictureUrl { get; set; }

        public string? Bio { get; set; }

        public Guid? LocationId { get; set; }
    }
}
