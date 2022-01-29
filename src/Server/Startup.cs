using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using VovaLantsovBlog.Data;
using VovaLantsovBlog.Server.Authentication;
using VovaLantsovBlog.Shared;

namespace VovaLantsovBlog.Server;

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
                builder.EnableRetryOnFailure()
                    .MigrationsAssembly("VovaLantsovBlog.Server")
                    .MigrationsHistoryTable("__AuthMigrationHistory", Constants.SchemaName)));

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddRoles<IdentityRole>()
            .AddRoleManager<RoleManager<IdentityRole>>()
            .AddEntityFrameworkStores<AuthDbContext>();
            
        services
            .AddAuthentication(options =>
            {
                options.DefaultScheme = options.DefaultAuthenticateScheme =
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt =>
            {
                var key = Encoding.ASCII.GetBytes(Configuration["Jwt:SecretKey"]);

                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = false,
                    ValidateLifetime = true
                };
            });
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