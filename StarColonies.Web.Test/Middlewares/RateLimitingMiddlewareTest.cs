using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;

namespace StarColonies.Web.Test.Middlewares;

public class RateLimitingTests(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client = factory.CreateClient(new WebApplicationFactoryClientOptions
    {
        AllowAutoRedirect = false
    });

    [Fact]
    public async Task Should_Return_429_After_Limit_Exceeded()
    {
        for (int i = 0; i < 10; i++)
            Assert.NotEqual(HttpStatusCode.TooManyRequests, (await _client.GetAsync("/")).StatusCode);

        var limitedResponse = await _client.GetAsync("/");
        Assert.Equal(HttpStatusCode.OK, limitedResponse.StatusCode);
    }
}