using HorseApp.Application.Common.Interfaces;
using System.Security.Claims;

namespace HorseApp.Api.Services
{
    public sealed class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Intern User ID (från database)
        /// Läggs till av UserSyncMiddleware
        /// </summary>
        public Guid UserId => Guid.Parse(GetRequiredClaim("user_id"));

        /// <summary>
        /// Email från Supabase token
        /// </summary>
        public string Email => GetRequiredClaim("email");

        /// <summary>
        /// Supabase Auth User ID
        /// </summary>

        private string GetRequiredClaim(string type)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user?.Identity?.IsAuthenticated != true)
                throw new Exception("User is not authenticated.");

            var claim = user.FindFirst(type);

            if (claim == null || string.IsNullOrWhiteSpace(claim.Value))
                throw new Exception($"Missing '{type}' claim.");

            return claim.Value;
        }
    }
}
