using FeedbackReporting.Application.Repositories;
using FeedbackReporting.Application.Services;
using FeedbackReporting.Domain.Repositories;
using FeedbackReporting.Domain.Services;
using InMemoryDatabase;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FeedbackReporting.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabase();
            services.AddRepositories();
            services.AddServices();
            return services;
        }

        public static void AddDatabase(this IServiceCollection services)
        {
            services.AddInMemoryDatabase();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IFeedbackRepository, FeedbackRepository>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IFeedbackService, FeedbackService>();
        }
    }
}
