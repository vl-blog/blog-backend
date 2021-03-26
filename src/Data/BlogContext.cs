using Microsoft.EntityFrameworkCore;
using VovaLantsovBlog.Shared.Models;

namespace VovaLantsovBlog.Data
{
    public sealed class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; } = null!;
        public DbSet<Tag> Tags { get; set; } = null!;
    }
}