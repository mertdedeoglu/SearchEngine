using Microsoft.AspNetCore.Mvc;
using SearchEngine.Application.Dtos;
using SearchEngine.Application.Interfaces;

namespace SearchEngine.Api.Controllers;

[ApiController]
[Route("api/search")]
public class SearchController : ControllerBase
{
    private readonly ISearchService _searchService;

    public SearchController(ISearchService searchService)
    {
        _searchService = searchService;
    }

    [HttpGet]
    public async Task<IActionResult> SearchAsync(
        [FromQuery] string? query,
        [FromQuery] string? sortBy,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] int? type = null)
    {
        var req = new SearchRequestDto
        {
            Query = query ?? string.Empty,
            SortBy = sortBy ?? "score",
            Page = page,
            PageSize = pageSize,
            TypeFilter = type.HasValue ? (SearchEngine.Domain.Enums.ContentType)type : null
        };

        var result = await _searchService.SearchAsync(req);

        return Ok(result);
    }
}
