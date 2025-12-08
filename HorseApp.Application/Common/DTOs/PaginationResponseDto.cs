
namespace HorseApp.Application.Common.DTOs
{
    public class PaginationResponseDto<T>
    {
        public int Page { get; set; }            // nuvarande sida
        public int PageSize { get; set; }        // antal per sida
        public int TotalCount { get; set; }      // totalt antal poster i DB
        public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
    }
}
