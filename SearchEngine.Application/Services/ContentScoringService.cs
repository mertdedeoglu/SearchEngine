using SearchEngine.Application.Interfaces;
using SearchEngine.Domain.Base;
using SearchEngine.Domain.Entities;
using SearchEngine.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Application.Services
{
    public class ContentScoringService : IContentScoringService
    {
        public double CalculateScore(ContentItem item)
        {
            double baseScore = item switch
            {
                VideoContent v => (v.Views / 1000.0) + (v.Likes / 100.0),
                TextContent t => t.ReadingTimeMinutes + (t.Reactions / 50.0),
                _ => 0
            };

            double typeMultiplier = item.Type == ContentType.Video ? 1.5 : 1.0;

            double recencyScore = CalculateRecencyScore(item.PublishedTime);

            double engagementScore = item switch
            {
                VideoContent v when v.Views > 0 => (v.Likes / v.Views ) * 10.0,
                TextContent t when t.ReadingTimeMinutes > 0 => (t.Reactions / t.ReadingTimeMinutes) * 5.0,
                _ => 0
            };
            return (baseScore * typeMultiplier) + recencyScore * engagementScore;
        }

        private double CalculateRecencyScore(DateTime publishedTime)
        {
            var days = (DateTime.Now - publishedTime).TotalDays;
            
            // Dokumandan anladigim kadariyla 1 hafta icinde olan guncel bir content diger secenekleri de kapsiyor.
            double totalScore = 0;
            if (days <= 7) totalScore += 5;
            if (days <= 30) totalScore += 3;
            if (days <= 90) totalScore += 1;
            return totalScore;
        }
    }
}
