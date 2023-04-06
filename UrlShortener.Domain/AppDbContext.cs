using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Models;

namespace UrlShortener.Domain
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ShortenUrl> ShortenUrls { get; set; }
    }
}
