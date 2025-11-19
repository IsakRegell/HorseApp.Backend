namespace HorseApp.Application.DTOs.Users
{
    public class CreateUserDto
    {
       public string Username { get; set; } = string.Empty;

       public string DisplayName { get; set; } = string.Empty;

       public string Email { get; set; } = string.Empty;

        public int Age { get; set; }

        public Guid? LocationId { get; set; }

        // Klartext-lösenord som skickas in från klienten
        // (hashar vi senare i backend, inte här)
        public string Password { get; set; } = string.Empty;
    }
}
