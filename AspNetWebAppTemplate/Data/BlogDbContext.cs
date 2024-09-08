using Microsoft.EntityFrameworkCore;

namespace AspNetWebAppTemplate.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext (DbContextOptions<BlogDbContext> options)
            : base(options)
        {
        }

        public DbSet<Models.User> Users { get; set; } = default!;
        public DbSet<Models.BlogPost> BlogPosts { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.User>().ToTable("User");
            modelBuilder.Entity<Models.BlogPost>().ToTable("BlogPost");
        }
    }
}
