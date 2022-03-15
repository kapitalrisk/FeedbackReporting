using FeedbackReporting.Domain.Models.Entities;
using FeedbackReporting.Domain.Models.Ressources;
using FeedbackReporting.Domain.Repositories;
using FeedbackReporting.Domain.UseCases;
using InMemoryDatabase.UseCasePattern;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackReporting.Application.UseCases
{
    public class CreateFeedbackUseCase : UseCaseBase<FeedbackRessource, int>, ICreateFeedbackUseCase
    {
        private readonly IFeedbackRepository _feedbackRepo;
        private readonly IFeedbackKeywordsRepository _feedbackKeywordsRepo;
        public override string ScopeKey => nameof(CreateFeedbackUseCase);

        public CreateFeedbackUseCase(ILogger<CreateFeedbackUseCase> logger, IUnitOfWork unitOfWork, IFeedbackRepository feedbackRepo, IFeedbackKeywordsRepository feedbackKeywordsRepo) : base(logger, unitOfWork)
        {
            _feedbackRepo = feedbackRepo;
            _feedbackKeywordsRepo = feedbackKeywordsRepo;
        }

        public override async Task<int> ActuallyExecuteAsync(FeedbackRessource ressource)
        {
            if (ressource == null)
                return -1;

            var insertedId = await _feedbackRepo.Insert(ressource.ToEntity());
            ressource.Id = insertedId;

            // Bad practice to perform this action here
            // This better be a background task
            await GenerateKeywords(ressource);

            return insertedId;
        }

        private async Task GenerateKeywords(FeedbackRessource ressource)
        {
            if (String.IsNullOrWhiteSpace(ressource.Description))
                return;

            var words = ressource.Description.Split(' ').Where(x => x.Length > 3);

            foreach (var word in words)
                await _feedbackKeywordsRepo.Insert(new FeedbackKeywordEntity { FeedbackId = ressource.Id, Keyword = word, HashCode = word.GetHashCode() });
        }
    }
}