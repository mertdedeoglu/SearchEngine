using SearchEngine.Domain.Base;
using SearchEngine.Domain.Entities;
using SearchEngine.Domain.Enums;
using SearchEngine.Domain.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SearchEngine.Infrastructure.Providers.Xml.Dtos;


namespace SearchEngine.Infrastructure.Providers.Xml
{
    public class XmlContentProvider : IContentProvider
    {
        private readonly HttpClient _client;

        public string Name => "XmlProvider";

        public XmlContentProvider(HttpClient client)
        {
            _client = client;
        }

        public async Task<IReadOnlyList<ContentItem>> FetchAsync(CancellationToken ct = default)
        {
            using var stream = await _client.GetStreamAsync("https://raw.githubusercontent.com/WEG-Technology/mock/refs/heads/main/v2/provider2", ct);

            var serializer = new XmlSerializer(typeof(XmlFeed));
            var feed = (XmlFeed?)serializer.Deserialize(stream);

            if (feed is null)
                return Array.Empty<ContentItem>();

            var list = new List<ContentItem>();

            foreach (var x in feed.Items.ItemList)
            {
                if (x.Type == "video")
                {
                    list.Add(new VideoContent
                    {
                        Id = Guid.NewGuid(),
                        ProviderName = Name,
                        ProviderItemId = x.Id,
                        Title = x.Headline,
                        Url = "#",
                        Description = string.Empty,
                        Type = ContentType.Video,
                        Views = x.Stats.Views,
                        Likes = x.Stats.Likes,
                        PublishedTime = DateTime.SpecifyKind(DateTime.Parse(x.PublicationDate), DateTimeKind.Utc)
                    });
                }
                else if (x.Type == "article")
                {
                    list.Add(new TextContent
                    {
                        Id = Guid.NewGuid(),
                        ProviderName = Name,
                        ProviderItemId = x.Id,
                        Title = x.Headline,
                        Url = "#",
                        Description = "",
                        Type = ContentType.Article,
                        ReadingTimeMinutes = x.Stats.ReadingTime ?? 0,
                        Reactions = x.Stats.Reactions ?? 0,
                        PublishedTime = DateTime.SpecifyKind(DateTime.Parse(x.PublicationDate), DateTimeKind.Utc)
                    });
                }
            }

            return list;
        }
    }
}
