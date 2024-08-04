using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace CRMEngSystem.Attributes.Cache
{
    public class ClearCacheAttribute : IAsyncActionFilter
    {
        private readonly IMemoryCache _memoryCache;

        public ClearCacheAttribute(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _memoryCache.Remove("contactName");
            _memoryCache.Remove("ordersList");
            _memoryCache.Remove("contactsList");
            await next();
        }
    }
}
