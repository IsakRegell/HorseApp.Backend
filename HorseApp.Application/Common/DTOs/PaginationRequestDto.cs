namespace HorseApp.Application.Common.DTOs
{
    public class PaginationRequestDto
    {
        public int Page { get; set; } = 1;       // vilken sida (1-baserad)
        public int PageSize { get; set; } = 20;  // hur många per sida
    }
}
