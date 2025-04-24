using System.Threading.RateLimiting;

namespace StarColonies.Web.Middlewares;

public static class RateLimitingMiddleware
{
    public static void AddFixedWindowRateLimiting(this IServiceCollection services)
        => services.AddRateLimiter(options =>
            {
                options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
                        factory: _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 10,
                            Window = TimeSpan.FromMinutes(1),
                            QueueLimit = 0,
                            AutoReplenishment = true
                        }));
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            });
}