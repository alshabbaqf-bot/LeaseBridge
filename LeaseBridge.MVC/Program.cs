using LeaseBridge.API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add MVC services
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();

// Register database context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// Serves files from wwwroot
app.MapStaticAssets();

app.UseRouting();

// Keep this only if authentication/login is configured in your project
app.UseAuthentication();
app.UseAuthorization();

// Management area route
// /Management will open Areas/Management/Controllers/DashboardController.cs -> Index()
app.MapControllerRoute(
    name: "management",
    pattern: "Management/{controller=Dashboard}/{action=Index}/{id?}",
    defaults: new { area = "Management" }
);

// Tenant area route
// /Tenant will open Areas/Tenant/Controllers/HomeController.cs -> Index()
app.MapControllerRoute(
    name: "tenant",
    pattern: "Tenant/{controller=Home}/{action=Index}/{id?}",
    defaults: new { area = "Tenant" }
);

// General area route
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

// Normal MVC route for controllers outside Areas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();