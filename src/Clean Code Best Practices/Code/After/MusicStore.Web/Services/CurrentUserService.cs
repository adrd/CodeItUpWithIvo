namespace MusicStore.Services
{
    using Microsoft.AspNetCore.Http;

    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor
                .HttpContext
                .User?
                .Identity
                .Name;
        }

        public string UserId { get; }
    }
}
