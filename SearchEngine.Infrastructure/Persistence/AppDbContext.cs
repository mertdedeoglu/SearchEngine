using Microsoft.EntityFrameworkCore;
using SearchEngine.Domain.Base;
using SearchEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<ContentItem> ContentItems => Set<ContentItem>();
        public DbSet<VideoContent> VideoContents => Set<VideoContent>();
        public DbSet<TextContent> TextContents => Set<TextContent>();

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContentItem>(entity =>
            {
                entity.ToTable("ContentItems");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.ProviderName)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.ProviderItemId)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.Title)
                    .HasMaxLength(500)
                    .IsRequired();

                entity.Property(e => e.Url)
                    .HasMaxLength(1000)
                    .IsRequired();

                entity.HasIndex(e => new { e.ProviderName, e.ProviderItemId })
                    .IsUnique();
            });

            modelBuilder.Entity<VideoContent>().ToTable("VideoContents");
            modelBuilder.Entity<TextContent>().ToTable("TextContents");
        }
    }
}
