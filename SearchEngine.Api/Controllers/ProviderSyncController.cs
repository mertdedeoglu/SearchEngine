using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SearchEngine.Infrastructure.Services;

namespace SearchEngine.Api.Controllers;

[EnableRateLimiting("ProviderSyncLimiter")]
[ApiController]
[Route("api/providers")]
public class ProviderSyncController : ControllerBase
{
    private readonly ProviderSyncService _syncService;

    public ProviderSyncController(ProviderSyncService syncService)
    {
        _syncService = syncService;
    }

    [HttpPost("sync")]
    public async Task<IActionResult> SyncAsync()
    {
        await _syncService.SyncAllAsync();
        return Ok(new { message = "Provider data synced successfully" });
    }
}