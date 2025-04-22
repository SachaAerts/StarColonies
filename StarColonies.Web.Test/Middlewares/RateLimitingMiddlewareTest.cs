using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;

namespace StarColonies.Web.Test.Middlewares;

public class RateLimitingMiddlewareTest(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient(new WebApplicationFactoryClientOptions
    {
        AllowAutoRedirect = false
    });

    [Fact]
    public async Task Should_Return429_When_RequestLimitExceeded()
    {
        for (int i = 0; i < 10; i++) Assert.True((await _client.GetAsync("/")).IsSuccessStatusCode);

        var blocked = await _client.GetAsync("/");
        Assert.Equal(HttpStatusCode.TooManyRequests, blocked.StatusCode);
    }
}