using FeedbackReporting.Application.Repositories;
using FeedbackReporting.Application.Services;
using FeedbackReporting.Application.UseCases;
using FeedbackReporting.Domain.Repositories;
using FeedbackReporting.Domain.Services;
using FeedbackReporting.Domain.UseCases;
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
            services.AddUseCases();
            return services;
        }

        private static void AddDatabase(this IServiceCollection services)
        {
            services.AddInMemoryDatabase();
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IFeedbackRepository, FeedbackRepository>();
            services.AddTransient<IFeedbackAttachmentRepository, FeedbackAttachmentRepository>();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IJWTService, JWTService>();
        }

        private static void AddUseCases(this IServiceCollection services)
        {
            services.AddTransient<ICreateFeedbackUseCase, CreateFeedbackUseCase>();
            services.AddTransient<IGetFeedbackByIdUseCase, GetFeedbackByIdUseCase>();
            services.AddTransient<IAttachDocumentToFeedbackUseCase, AttachDocumentToFeedbackUseCase>();
        }
    }
}
