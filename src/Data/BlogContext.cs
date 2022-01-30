using Microsoft.EntityFrameworkCore;
using VovaLantsovBlog.Shared.Models;
#pragma warning disable CS8618

namespace VovaLantsovBlog.Data;

public sealed class BlogContext : DbContext
{
    public BlogContext(DbContextOptions<BlogContext> options) : base(options)
    {
    }

    public DbSet<Post> Posts { get; set; }
    public DbSet<Tag> Tags { get; set; }
}