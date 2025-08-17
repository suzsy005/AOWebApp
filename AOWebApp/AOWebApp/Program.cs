using AOWebApp.Data;
using Microsoft.Build.Execution;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AmazonOrders2025Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AmazonOrders2025Context") ??
    throw new InvalidOperationException("Connection string 'AOWebAppContext' not found.")));

//// AmazonOrdersContext
//builder.Services.AddDbContext<AmazonOrdersContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("AmazonOrdersContext") ??
//    throw new InvalidOperationException("Connection string 'AmazonOrdersContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
