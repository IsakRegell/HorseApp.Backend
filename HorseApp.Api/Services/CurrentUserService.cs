using HorseApp.Application.Common.Interfaces;
using System.Security.Claims;

namespace HorseApp.Api.Services
{
    public sealed class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _http;

        public CurrentUserService(IHttpContextAccessor http)
        {
            _http = http;
        }

        public bool IsAuthenticated
        {
            get
            {
                var user = _http.HttpContext?.User;
                return user?.Identity?.IsAuthenticated == true;
            }
        }

        public Guid UserId
        {
            get
            {
                var user = _http.HttpContext?.User;

                var sub = user?.FindFirstValue("sub")
                          ?? user?.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrWhiteSpace(sub))
                    return Guid.Empty;

                return Guid.TryParse(sub, out var id) ? id : Guid.Empty;
            }
        }
    }
}
