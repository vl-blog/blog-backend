using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VovaLantsovBlog.Data;
using VovaLantsovBlog.Server.Authentication;

namespace VovaLantsovBlog.Server
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (IServiceScope scope = host.Services.CreateScope())
            {
                IServiceProvider provider = scope.ServiceProvider;
                BlogContext context = provider.GetRequiredService<BlogContext>();
                AuthDbContext authContext = provider.GetRequiredService<AuthDbContext>();

                if (context.Database.GetPendingMigrations().Any())
                    context.Database.Migrate();
                if (authContext.Database.GetPendingMigrations().Any())
                    authContext.Database.Migrate();
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) =>
                {
                    string env = context.HostingEnvironment.EnvironmentName;
                    builder.AddJsonFile("dbsettings.json", false, false)
                        .AddJsonFile($"dbsettings.{env}.json", true, false);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
