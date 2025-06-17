using ClassTask.Configurations;
using ClassTask.Models;
using Microsoft.EntityFrameworkCore;

namespace ClassTask.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<MediaUser> MediaUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MediaUserEntityTypeConfiguration());
        }
    }
}
