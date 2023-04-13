using DocumentStorageMVC.Core;
using DocumentStorageMVC.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DocumentStorageMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var identityConnectionString = builder.Configuration.GetConnectionString("IdentityDbConnection")
                ?? throw new InvalidOperationException("Connection string 'IdentityDbConnection' not found.");

            var appConnectionString = builder.Configuration.GetConnectionString("ApplicationDbConnection")
                ?? throw new InvalidOperationException("Connection string 'ApplicationDbConnection' not found.");

            builder.Services.AddDbContext<ApplicationIdentityDbContext>(options =>
                options.UseSqlServer(identityConnectionString));

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(appConnectionString));

            builder.Services.AddScoped<IRepository<Document>, DocumentRepository>();

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>();
            builder.Services.AddControllersWithViews();

            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

            builder.Services.AddMediatR(c => c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            var app = builder.Build();

            DirectoryInfo dirInfo = new DirectoryInfo(app.Environment.WebRootPath + "/Files/");
            if (!dirInfo.Exists) dirInfo.Create();

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
            app.UseStaticFiles();

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