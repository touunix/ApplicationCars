using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ApplicationCars.Data;
using ApplicationCars.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddAuthentication().AddCookie("Cookie", options =>
{
    options.Cookie.Name = "Cookie";
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("IsMechanic", policy => policy.RequireClaim("Job", "Mechanic"));
    options.AddPolicy("IsAdmin", policy => policy.RequireClaim("Job", "Admin"));
});

builder.Services.AddDbContext<ApplicationCarsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationCarsContext") ?? throw new InvalidOperationException("Connection string 'ApplicationCarsContext' not found.")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
