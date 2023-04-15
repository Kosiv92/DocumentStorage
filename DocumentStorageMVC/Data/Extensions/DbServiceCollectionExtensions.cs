using DocumentStorageMVC.Core;
using Microsoft.EntityFrameworkCore;

namespace DocumentStorageMVC.Data
{ 
    public static class DbServiceCollectionExtensions
    {
        public static IServiceCollection AddDbConnections(
            this IServiceCollection services, ConfigurationManager configurationManager)
        {
            var identityConnectionString = configurationManager.GetConnectionString("IdentityDbConnection")
                ?? throw new InvalidOperationException("Connection string 'IdentityDbConnection' not found.");

            var appConnectionString = configurationManager.GetConnectionString("ApplicationDbConnection")
                ?? throw new InvalidOperationException("Connection string 'ApplicationDbConnection' not found.");

            services.AddDbContext<ApplicationIdentityDbContext>(options =>
                options.UseSqlServer(identityConnectionString));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(appConnectionString));

            services.AddScoped<IRepository<Document>, DocumentRepository>();

            services.AddDatabaseDeveloperPageExceptionFilter();

            return services;
        }
    }
}