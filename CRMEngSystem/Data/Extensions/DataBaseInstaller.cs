using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CRMEngSystem.Data.Context;

namespace CRMEngSystem.Data.Extensions
{
    public static class DataBaseInstaller
    {
        public static IServiceCollection AddPostgreSQLDataBase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContextPool<CRMEngSystemDbContext>(options =>
                options.UseNpgsql(connectionString, builder => builder.MigrationsAssembly("CRM-EngSystem-DataBase")));

            return services;
        }

        public static IServiceCollection AddMSSqlServerDataBase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContextPool<CRMEngSystemDbContext>(options =>
                options.UseSqlServer(connectionString, builder => builder.MigrationsAssembly("CRM-EngSystem-DataBase")));

            return services;
        }
    }
}
