using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using VovaLantsovBlog.Data;
using VovaLantsovBlog.Server.Authentication;
using VovaLantsovBlog.Shared;

var builder = WebApplication.CreateBuilder(args);

string env = builder.Environment.EnvironmentName;
builder.Configuration
	.AddJsonFile("dbsettings.json", false, false)
	.AddJsonFile($"dbsettings.{env}.json", true, false)
	.AddJsonFile("sentrysettings.json", false, false)
	.AddJsonFile($"sentrysettings.{env}.json", true, false)
	.AddJsonFile("jwt.json", false, false)
	.AddJsonFile($"jwt.{env}.json", true, false);

builder.Logging.AddConfiguration(builder.Configuration);
builder.Logging.AddDebug();
builder.Logging.AddConsole();

builder.WebHost.UseSentry();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDbContext<BlogContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString("BlogConnectionString"), npgsqlBuilder =>
		npgsqlBuilder.EnableRetryOnFailure()
			.MigrationsAssembly("VovaLantsovBlog.Data")
			.MigrationsHistoryTable("__MigrationHistory", Constants.SchemaName)));
builder.Services.AddDbContext<AuthDbContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString("BlogConnectionString"), npgsqlBuilder =>
		npgsqlBuilder.EnableRetryOnFailure()
			.MigrationsAssembly("VovaLantsovBlog.Server")
			.MigrationsHistoryTable("__AuthMigrationHistory", Constants.SchemaName)));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
	.AddRoles<IdentityRole>()
	.AddRoleManager<RoleManager<IdentityRole>>()
	.AddEntityFrameworkStores<AuthDbContext>();

builder.Services.AddAuthentication(options =>
	{
		options.DefaultScheme = options.DefaultAuthenticateScheme =
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	})
	.AddJwtBearer(jwt =>
	{
		var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:SecretKey"]);

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

var app = builder.Build();

if (app.Environment.IsDevelopment())
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