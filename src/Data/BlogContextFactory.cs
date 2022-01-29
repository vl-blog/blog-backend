using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace VovaLantsovBlog.Data;

public sealed class BlogContextFactory : IDesignTimeDbContextFactory<BlogContext>
{
    public BlogContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BlogContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;UserId=postgres;Password=pwd;Database=blog;CommandTimeout=300;");

        return new BlogContext(optionsBuilder.Options);
    }
}