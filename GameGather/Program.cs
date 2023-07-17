using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Seed;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add seeders to the container
builder.Services.AddScoped<SeedData>()
        .AddScoped<SeedDataIdentity>();

// Database connection
builder.Services.AddDbContext<GameGatherDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GameGatherDbConnection")));

// Identity
builder.Services.AddDbContext<IdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UserAccountbConnection"))
    .EnableSensitiveDataLogging(true));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<IdentityDbContext>()
    .AddDefaultTokenProviders();

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<AccountDbContext>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.Cookie.Name = "Identity.Cookie";
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
    });

// Configure cookie policy
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.Strict;
});

// Configure session
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(1);
    options.Cookie.HttpOnly = true;
});

var app = builder.Build();

// Resolve GameGatherDbContext from the service provider
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<GameGatherDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
} else
{
    // Only in dev env seed with dummy data -->
    SeedDatabase();
} 

app.UseStaticFiles();

app.UseRouting();

app.UseCookiePolicy();

app.UseSession();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

// Functionality to seed the db with dummy data -->
void SeedDatabase()
{
    using var scope = app.Services.CreateScope();
    var dbBoarGameSeeder = scope.ServiceProvider.GetRequiredService<SeedData>();
    dbBoarGameSeeder.EnsurePopulated(true);

    var dbIdentitySeeder = scope.ServiceProvider.GetRequiredService<SeedDataIdentity>();
    dbIdentitySeeder.EnsurePopulated(true);

}
