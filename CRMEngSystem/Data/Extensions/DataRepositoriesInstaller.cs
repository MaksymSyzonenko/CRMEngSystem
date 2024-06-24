using Microsoft.Extensions.DependencyInjection;
using CRMEngSystem.Data.Repositories.Core;
using CRMEngSystem.Data.Repositories.Factory;


namespace CRM_EngSystem_DataBase.Extensions
{
    public static class DataRepositoriesInstaller
    {
        public static IServiceCollection AddDataRepositories(this IServiceCollection services)
        {
            services
                .AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services
                .AddScoped<IRepositoryFactory, RepositoryFactory>();

            return services;
        }
    }
}
