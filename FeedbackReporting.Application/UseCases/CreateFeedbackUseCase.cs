using FeedbackReporting.Domain.Models.Ressources;
using FeedbackReporting.Domain.Repositories;
using FeedbackReporting.Domain.UseCases;
using InMemoryDatabase.UseCasePattern;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FeedbackReporting.Application.UseCases
{
    public class CreateFeedbackUseCase : UseCaseBase<FeedbackRessource, int>, ICreateFeedbackUseCase
    {
        private readonly IFeedbackRepository _feedbackRepo;
        public override string ScopeKey => nameof(CreateFeedbackUseCase);

        public CreateFeedbackUseCase(ILogger<CreateFeedbackUseCase> logger, IUnitOfWork unitOfWork, IFeedbackRepository feedbackRepo) : base(logger, unitOfWork)
        {
            _feedbackRepo = feedbackRepo;
        }

        public override async Task<int> ActuallyExecuteAsync(FeedbackRessource entityToInsert)
        {
            return await _feedbackRepo.Insert(entityToInsert.ToEntity());
        }
    }
}