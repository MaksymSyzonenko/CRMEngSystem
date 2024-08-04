using CRMEngSystem.Data.Repositories.Core;
using CRMEngSystem.Data.Repositories.Factory;


namespace CRMEngSystem.Data.Extensions
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
