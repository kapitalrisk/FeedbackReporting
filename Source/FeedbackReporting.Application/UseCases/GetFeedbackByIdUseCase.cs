using FeedbackReporting.Domain.Models.Ressources;
using FeedbackReporting.Domain.Repositories;
using FeedbackReporting.Domain.UseCases;
using InMemoryDatabase.UseCasePattern;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FeedbackReporting.Application.UseCases
{
    public class GetFeedbackByIdUseCase : UseCaseBase<int, FeedbackRessource>, IGetFeedbackByIdUseCase
    {
        private readonly IFeedbackRepository _feedbackRepo;

        public override string ScopeKey => nameof(GetFeedbackByIdUseCase);

        public GetFeedbackByIdUseCase(ILogger<GetFeedbackByIdUseCase> logger, IUnitOfWork unitOfWork, IFeedbackRepository feedbackRepo) : base(logger, unitOfWork)
        {
            _feedbackRepo = feedbackRepo;
        }

        public override async Task<FeedbackRessource> ActuallyExecuteAsync(int id)
        {
            var result = await _feedbackRepo.GetById(id);
            return result != null ? new FeedbackRessource(result) : null;
        }
    }
}
