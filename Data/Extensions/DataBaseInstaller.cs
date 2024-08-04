using Microsoft.EntityFrameworkCore;
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
    }
}
