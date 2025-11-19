using SearchEngine.Application.Dtos;
using SearchEngine.Application.Services;
using SearchEngine.Domain.Base;
using SearchEngine.Domain.Entities;
using SearchEngine.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Tests.Application
{
    public class SearchServiceTests
    {
        [Fact]
        public async Task IsCorrectSearchQueryFilter()
        {
            var items = new List<ContentItem>
            {
                new VideoContent { Id= Guid.NewGuid(), Title="Go Programming", Description = "",  FinalScore=10 },
                new VideoContent { Id= Guid.NewGuid(), Title="Docker Tutorial", Description = "", FinalScore=10 }
            };

            var query = new FakeContentQuery(items);

            var service = new SearchService(query);

            var result = await service.SearchAsync(new SearchRequestDto
            {
                Query = "go",
                Page = 1,
                PageSize = 10
            });

            // Assert
            Assert.Single(result.Items);
            Assert.Equal("Go Programming", result.Items[0].Title);
            Assert.Equal(10, result.Items[0].Score);
        }

    }
}
