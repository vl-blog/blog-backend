using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VovaLantsovBlog.Data;
using VovaLantsovBlog.Server.Authentication;
using VovaLantsovBlog.Shared;

namespace VovaLantsovBlog.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();
            
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDbContext<BlogContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("BlogConnectionString"), builder =>
                    builder.EnableRetryOnFailure()
                        .MigrationsAssembly("VovaLantsovBlog.Data")
                        .MigrationsHistoryTable("__MigrationHistory", Constants.SchemaName)));

            services.AddDbContext<AuthDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("BlogConnectionString"), builder =>
                    builder.EnableRetryOnFailure()));
            
            services.AddDefaultIdentity<ApplicationUser>()
                .AddEntityFrameworkStores<AuthDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, AuthDbContext>();
            
            services.AddAuthentication()
                .AddIdentityServerJwt();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
