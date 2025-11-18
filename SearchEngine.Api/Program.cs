using Microsoft.EntityFrameworkCore;
using Scrutor;
using SearchEngine.Application.Interfaces;
using SearchEngine.Application.Services;
using SearchEngine.Domain.Providers;
using SearchEngine.Infrastructure.Persistence;
using SearchEngine.Infrastructure.Providers;
using SearchEngine.Infrastructure.Providers.Json;
using SearchEngine.Infrastructure.Providers.Xml;
using SearchEngine.Infrastructure.Queries;
using SearchEngine.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// Memory cache
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<ICacheService, MemoryCacheService>();

// Application Services
builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.Decorate<ISearchService, CachedSearchService>();

builder.Services.AddScoped<IContentScoringService, ContentScoringService>();
builder.Services.AddScoped<IContentQuery, EFContentQuery>();

// Provider Services
builder.Services.AddHttpClient<JsonContentProvider>(client =>
{
    client.BaseAddress = new Uri("https://raw.githubusercontent.com/WEG-Technology/mock/refs/heads/main/v2/");
});

builder.Services.AddHttpClient<XmlContentProvider>(client =>
{
    client.BaseAddress = new Uri("https://raw.githubusercontent.com/WEG-Technology/mock/refs/heads/main/v2/");
});
builder.Services.AddScoped<IContentProvider, JsonContentProvider>();
builder.Services.AddScoped<IContentProvider, XmlContentProvider>();

// Sync service
builder.Services.AddScoped<ProviderSyncService>();

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
