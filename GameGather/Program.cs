using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Seed;
using Microsoft.AspNetCore.Authentication.Cookies;
using DomainServices;
using Infrastructure.Repos;
using NToastNotify;
using Web.MappingProfiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Set the repository to use for dependency injection
builder.Services.AddScoped<IBoardGameNightRepository, BoardGameNightRepository>();
builder.Services.AddScoped<IBoardGameRepository, BoardGameRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IFoodAndDrinksPrefRepository, FoodAndDrinksPrefRepository>();

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

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;
})
    .AddEntityFrameworkStores<IdentityDbContext>()
    .AddDefaultTokenProviders();

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
    options.MinimumSameSitePolicy = SameSiteMode.Strict;
});

// Configure session
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(1);
    options.Cookie.HttpOnly = true;
});

// Add ntoastnotify service for notifications
builder.Services.AddMvcCore().AddNToastNotifyToastr(new ToastrOptions()
{
    ProgressBar = true,
    PositionClass = ToastPositions.BottomCenter,
    TimeOut = 3000,
    ExtendedTimeOut = 3000,
});

builder.Services.AddAutoMapper(typeof(MappingProfile));

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

app.UseNToastNotify();

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
