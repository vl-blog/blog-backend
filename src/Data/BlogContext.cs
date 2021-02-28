using Microsoft.EntityFrameworkCore;

namespace VovaLantsovBlog.Data
{
    public sealed class BlogContext : DbContext
    {
        public const string SchemaName = "public";
        
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
        }
    }
}