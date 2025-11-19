using System.Net;
using System.Threading.Tasks;
using SearchEngine.Tests.Api;
using Xunit;

public class ProviderSyncTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ProviderSyncTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task ProviderSyncReturn200()
    {
        var response = await _client.PostAsync("/api/providers/sync", null);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
