using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SustainabotMVCContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SustainabotMVCContext") ?? throw new InvalidOperationException("Connection string 'SustainabotMVCContext' not found.")));

// Set up review database
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<SustainabotMVCContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("SustainabotMVCContext")));
}
else
{
    builder.Services.AddDbContext<SustainabotMVCContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("ProductionSustainabotMVCContext")));
}

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
