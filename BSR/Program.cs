using BSR.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<HomeService>();
builder.Services.AddRazorPages();

var app = builder.Build();

app.UseStaticFiles();

app.MapRazorPages();

app.Run();