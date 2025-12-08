namespace HorseApp.Application.DTOs.Users
{
    // DTO för listning av users (t.ex. i en lista eller feed)
    public class UserListItemDto
    {
        public Guid Id { get; set; }                 // unik identifierare
        public string Username { get; set; } = default!;
        public string DisplayName { get; set; } = default!;
        public int Age { get; set; }
        public string? ProfileImageUrl { get; set; } // räcker bra i listor
    }
}
