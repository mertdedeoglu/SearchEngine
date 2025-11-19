using Microsoft.Extensions.Logging;
using SearchEngine.Domain.Base;
using SearchEngine.Domain.Entities;
using SearchEngine.Domain.Enums;
using SearchEngine.Domain.Providers;
using SearchEngine.Infrastructure.Providers.Json.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SearchEngine.Infrastructure.Providers.Json
{

    public class JsonContentProvider : IContentProvider
    {
        private readonly HttpClient _client;
        private readonly ILogger<JsonContentProvider> _logger;


        public string Name => "JsonProvider";

        public JsonContentProvider(HttpClient client, ILogger<JsonContentProvider> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<IReadOnlyList<ContentItem>> FetchAsync(CancellationToken ct = default)
        {
            string url = "https://raw.githubusercontent.com/WEG-Technology/mock/refs/heads/main/v2/provider1";
            try
            {
                using var stream = await _client.GetStreamAsync(url, ct);

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                var data = await JsonSerializer.DeserializeAsync<JsonContentResponse>(stream, options, ct);

                if (data is null || data.Contents.Count == 0)
                    return Array.Empty<ContentItem>();

                var list = new List<ContentItem>();

                foreach (var x in data.Contents)
                {
                    if (x.Type == "video")
                    {
                        list.Add(new VideoContent
                        {
                            Id = Guid.NewGuid(),
                            ProviderName = Name,
                            ProviderItemId = x.Id,
                            Title = x.Title,
                            Url = "#",
                            Description = "",
                            Type = ContentType.Video,
                            Views = x.Metrics.Views,
                            Likes = x.Metrics.Likes,
                            PublishedTime = DateTime.SpecifyKind(x.PublishedAt, DateTimeKind.Utc)
                        });
                    }
                    else if (x.Type == "article")
                    {
                        list.Add(new TextContent
                        {
                            Id = Guid.NewGuid(),
                            ProviderName = Name,
                            ProviderItemId = x.Id,
                            Title = x.Title,
                            Url = "#",
                            Description = "",
                            Type = ContentType.Article,
                            ReadingTimeMinutes = x.Metrics.ReadingTime,
                            Reactions = x.Metrics.Reactions,
                            PublishedTime = DateTime.SpecifyKind(x.PublishedAt, DateTimeKind.Utc)
                        });
                    }
                }

                return list;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "JSON Provider bağlantı hatası: {Url}", url);
                return new List<ContentItem>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "JSON Provider'da beklenmeyen bir hata.");
                return new List<ContentItem>();
            }

        }
    }
}

