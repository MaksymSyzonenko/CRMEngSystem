using CRMEngSystem.Data.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;

namespace CRMEngSystem.Services.Cache.User
{
    public sealed class UserCacheService
    {
        private readonly IMemoryCache _cache;
        private readonly UserManager<UserEntity> _userManager;

        public UserCacheService(IMemoryCache cache, UserManager<UserEntity> userManager)
        {
            _cache = cache;
            _userManager = userManager;
        }

        public async Task<string?> GetUserIdAsync(string username)
        {
            if (_cache.TryGetValue(username, out string? userId))
            {
                return userId;
            }

            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                userId = user.Id;
                _cache.Set(username, userId, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15)
                });
            }

            return userId;
        }
    }
}
