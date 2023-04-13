using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System.IO;

namespace DocumentStorage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(option =>
                {
                    option.LoginPath = "/Auth/Login";
                    option.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                });
                        
            var app = builder.Build();

            DirectoryInfo dirInfo = new DirectoryInfo(app.Environment.WebRootPath + "/Files/");
            if (!dirInfo.Exists) dirInfo.Create();

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
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                //pattern: "{controller=Auth}/{action=Login}/{id?}");
                pattern: "{controller=Account}/{action=Register}/{id?}");

            //app.MapDefaultControllerRoute();

            app.Run();
        }
    }
}