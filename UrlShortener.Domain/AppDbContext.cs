using System.Runtime.CompilerServices;
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
            var adminUser = new User { Id = 1, Email = "admin@gmail.com", Password = "2af9b1ba42dc5eb01743e6b3759b6e4b", Role = Role.Admin};
            var about = new AboutMessage() { Id = 1, Message = @"
To short url I used a Bijective Function f. 
If you want more, visit <a>https://en.wikipedia.org/wiki/Bijection</a>" };

            modelBuilder.Entity<User>().HasData(new List<User> { adminUser });
            modelBuilder.Entity<AboutMessage>().HasData(new List<AboutMessage> { about });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ShortenUrl> ShortenUrls { get; set; }
        public DbSet<AboutMessage> AboutMessages { get; set; }
    }
}
