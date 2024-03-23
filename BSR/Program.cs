using BSR.Models;
using BSR.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Seq("http://localhost:5341")
    .CreateLogger();

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddSeq(builder.Configuration.GetSection("Seq"));
});

builder.Services.AddDbContext<HomeContext>(opt => opt.UseSqlite("Data Source=bsr.db"));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 5;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.SignIn.RequireConfirmedEmail = false;
})
    .AddEntityFrameworkStores<HomeContext>()
    .AddDefaultTokenProviders();

// new code 
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login";
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.HttpOnly = true;
});

builder.Services.AddScoped<HomeService>();
builder.Services.AddScoped<AddressService>();
builder.Services.AddScoped<DataSeedService>();
builder.Services.AddHttpClient();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(options =>
{
    options.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute());
});

var app = builder.Build();

app.UseHttpsRedirection(); 

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<HomeContext>();
    //dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();

    var dataSeedService = scope.ServiceProvider.GetRequiredService<DataSeedService>();
    dataSeedService.SeedHomes();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization(); 

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Homes}/{action=Index}/{id?}");
});

app.Run();
