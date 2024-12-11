using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Reflection;
using System.Security.Principal;
using YourAnimeList.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace YourAnimeList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("YourAnimeList") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            //This line adds a developer-specific exception filter for database-related exceptions.
            //When enabled, if there’s a database - related error(e.g., a migration is missing or a
            //connection fails), the application will show a detailed error page during development,
            //helping you quickly identify and fix the problem.
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            //AddDefaultIdentity<IdentityUser>:
            //Registers default ASP.NET Core Identity services for managing user authentication and authorization.
            //options => options.SignIn.RequireConfirmedAccount = true:
            //This line configures the sign-in behavior, requiring users to confirm their email before they can log in.
            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //AddEntityFrameworkStores<ApplicationDbContext>():
            //Specifies that Identity should use Entity Framework Core (EF Core) with ApplicationDbContext to 
            //store and manage user data (e.g., user accounts, roles, claims).
            .AddEntityFrameworkStores<ApplicationDbContext>();

            // Registers MVC services
            builder.Services.AddControllersWithViews(); 

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();// Middleware for serving static files

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
