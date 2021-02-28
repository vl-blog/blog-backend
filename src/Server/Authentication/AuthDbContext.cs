using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace VovaLantsovBlog.Server.Authentication
{
    public sealed class AuthDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public AuthDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions)
            : base(options, operationalStoreOptions)
        {
        }
    }
}