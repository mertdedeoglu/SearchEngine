using Microsoft.AspNetCore.Mvc.RazorPages;
using SearchEngine.Application.Dtos;
using SearchEngine.Tests.Api;
using System.Globalization;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

public class SearchEndpointTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public SearchEndpointTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient(); // gerçek API gibi çalışacak
    }

    [Fact]
    public async Task Search200Ok()
    {
        var response = await _client.GetAsync("/api/search?query=go&page=1&pageSize=10&typeFilter=1&sortBy=score");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task SearchReturnValidPayload()
    {

        var response = await _client.GetAsync("/api/search?query=&page=1&pageSize=5&typeFilter=1&sortBy=score");
        var result = await response.Content.ReadFromJsonAsync<SearchResultDto>();

        Assert.NotNull(result);
        Assert.True(result.Items.Count <= 5);
    }
}
