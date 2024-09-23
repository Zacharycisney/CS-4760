using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CS4760Group1.Data;
using CS4760Group1.Models;
using Microsoft.AspNetCore.Identity;
using CS4760Group1.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<CS4760Group1Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CS4760Group1Context") ?? throw new InvalidOperationException("Connection string 'CS4760Group1Context' not found.")));

//builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<CS4760Group1Context>();
builder.Services.AddScoped<IPasswordHasher<User>, PlainTextPasswordHasher<User>>();
builder.Services.AddDefaultIdentity<User>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;  // Disable email confirmation
    options.Password.RequireNonAlphanumeric = false; // Adjust password rules as needed
})
.AddEntityFrameworkStores<CS4760Group1Context>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3;  

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
    options.Lockout.MaxFailedAccessAttempts = 10;
    options.Lockout.AllowedForNewUsers = false;

    options.User.RequireUniqueEmail = true;
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<CS4760Group1Context>();

    try
    {
        // Try to query the database to see if it works
        if (context.College.Any())
        {
            Console.WriteLine("Database connected successfully.");
        }
        else
        {
            Console.WriteLine("Database connected, but no records found.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error connecting to the database: {ex.Message}");
    }
    //SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


// sets default page to login if user is not authenticated
//app.Use(async (context, next) =>
//{
//    // Allow access to the login and register pages without authentication
//    var path = context.Request.Path.Value;
//    if (!context.User.Identity.IsAuthenticated &&
//        !path.StartsWith("/Identity/Account/Login") &&
//        !path.StartsWith("/Identity/Account/Register") &&
//        !path.StartsWith("/Identity/Account/ForgotPassword"))
//    {
//        context.Response.Redirect("/Identity/Account/Login");
//        return;
//    }
//    await next();
//});

app.UseRouting();

app.UseAuthentication(); // part of identity framework
app.UseAuthorization();

app.Use(async (context, next) =>
{
    var path = context.Request.Path.Value;

    // Check if the user is authenticated
    if (!context.User.Identity.IsAuthenticated)
    {
        // Allow access to the login and register pages without authentication
        if (path.StartsWith("/Identity/Account/Login") ||
            path.StartsWith("/Identity/Account/Register") ||
            path.StartsWith("/Identity/Account/ForgotPassword") ||
            path.StartsWith("/Identity/Account/ResetPassword"))
        {
            await next(); // Allow access to these pages
            return;
        }

        // Redirect to login for all other pages
        context.Response.Redirect("/Identity/Account/Login");
        return;
    }

    // If authenticated, just proceed
    await next();
});

app.MapRazorPages();

app.Run();
