using SearchEngine.Application.Services;
using SearchEngine.Domain.Entities;

namespace SearchEngine.Tests.Application
{
    public class ContentScoringServiceTests
    {
        private readonly ContentScoringService _service;

        public ContentScoringServiceTests()
        {
            _service = new ContentScoringService();
        }

        [Fact]
        public void IsCorrectVideoTypeFinalScore()
        {
            //Video Item
            var video = new VideoContent
            {
                Views = 10000, // 10000 / 1000 = 10
                Likes = 500, // 500 / 100 = 5
                PublishedTime = DateTime.Now.AddDays(-3),  // 1 haftanın içinde
                Type = Domain.Enums.ContentType.Video
            };

            // Calculate
            var score = _service.CalculateScore(video);

            Assert.True(score > 0);
            Assert.Equal(31.5, score);  
        }
    }
}
