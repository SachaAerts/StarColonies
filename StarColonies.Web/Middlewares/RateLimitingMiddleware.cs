using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;

namespace StarColonies.Web.Middlewares;

/**
 * `RateLimitingMiddleware` protège des attaques DoS qui limite l'envoie
 * abusive de requête. Cela pourrait causé une surchage au niveau du serveur
 * et le faire planté. 
 */
public class RateLimitingMiddleware(RequestDelegate next, IMemoryCache cache)
{
    private const int MaxRequestsPerInterval = 10;
    private static readonly TimeSpan Interval = TimeSpan.FromSeconds(10);
    
    /**
     * - Obtenir l'adresse ip de l'utilisateur connecté,
     * - Compter le nombre de requête émise par le client dépassant les 10sec d'intervalles,
     * - Intervenir en cas >= à 10
     */
    public async Task InvokeAsync(HttpContext context)
    {
        var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        var cacheKey = $"RateLimit-{ip}";

        int currentRequestCount = cache.GetOrCreate(cacheKey, entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = Interval;
            entry.AddExpirationToken(new CancellationChangeToken(new CancellationTokenSource(Interval).Token));
            return 0;
        });

        if (currentRequestCount >= MaxRequestsPerInterval)
        {
            context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
            await context.Response.WriteAsync("Trop de requêtes, réessayez plus tard.");
            return;
        }

        cache.Set(cacheKey, currentRequestCount + 1, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = Interval
        });

        await next(context);
    }
    
}

public static class RateLimitingMiddlewareExtensions
{
    public static IApplicationBuilder UseRateLimiting(this IApplicationBuilder builder)
        => builder.UseMiddleware<RateLimitingMiddleware>();
}
