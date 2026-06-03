using LeaseBridge.Reporting.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure cookie authentication
builder.Services
     .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
     .AddCookie(options =>
     {
         options.LoginPath = "/Account/Login";
         options.AccessDeniedPath = "/Account/AccessDenied";
         options.ExpireTimeSpan = TimeSpan.FromHours(1);
         options.SlidingExpiration = false;
     });

// Register HttpContextAccessor and ReportingApiClient
builder.Services.AddHttpContextAccessor(); // httpContextAccessor is needed for accessing the current user's information in the ReportingApiClient
builder.Services.AddHttpClient<ReportingApiClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7010/");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication(); // Enable authentication middleware
app.UseAuthorization();

app.MapStaticAssets();

/* 
 * Set up the default route to point to the Account controller's Login action
 * so the application will redirect to the login page when accessed without a specific route.
*/
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}")
    .WithStaticAssets();


app.Run();
