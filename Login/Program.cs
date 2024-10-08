using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Login.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("LoginContextConnection") ?? throw new InvalidOperationException("Connection string 'LoginContextConnection' not found.");

builder.Services.AddDbContext<LoginContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<LoginUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<LoginContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.MapRazorPages();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
