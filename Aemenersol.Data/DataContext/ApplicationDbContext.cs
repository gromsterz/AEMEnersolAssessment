using Aemenersol.Entity;
using Microsoft.EntityFrameworkCore;

namespace Aemenersol.Data.DataContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Well> Wells { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Platform>()
                .HasMany(x => x.Wells)
                .WithOne(x => x.Platform)
                .HasForeignKey(x => x.PlatformId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}