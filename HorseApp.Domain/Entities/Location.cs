namespace HorseApp.Domain.Entities
{
    public class Location
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; } = string.Empty;

        public string? Region { get; set; }

        public string CountryCode { get; set; } = "SE";

        public bool IsActive { get; set; } = true;
    }
}
