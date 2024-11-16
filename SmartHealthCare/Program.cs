using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartHealthCare.DataAccess; // Adjust this namespace based on your project structure

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<DatabaseRepository>(); // Register DatabaseRepository for dependency injection

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Redirect to error page in non-development environments
    app.UseHsts(); // Enforce HTTPS in production
}

app.UseHttpsRedirection(); // Redirect HTTP requests to HTTPS
app.UseStaticFiles(); // Enable serving static files from wwwroot folder

app.UseRouting(); // Enable routing middleware

app.UseAuthorization(); // Enable authorization middleware

// Define the default route for the application
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();