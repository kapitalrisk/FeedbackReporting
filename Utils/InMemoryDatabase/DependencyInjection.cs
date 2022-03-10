using InMemoryDatabase.UseCasePattern;
using Microsoft.Extensions.DependencyInjection;

namespace InMemoryDatabase
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInMemoryDatabase(this IServiceCollection services)
        {
            services.AddTransient<IDefaultRepository, DefaultRepository>();
            services.AddTransient<IDatabaseGenerator, DatabaseGenerator>();
            services.AddSingleton<IInMemoryDatabaseConnectionFactory, InMemoryDatabaseConnectionFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            return services;
        }
    }
}
