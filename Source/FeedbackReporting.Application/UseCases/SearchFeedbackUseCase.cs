using FeedbackReporting.Domain.Models.Ressources;
using FeedbackReporting.Domain.Repositories;
using FeedbackReporting.Domain.UseCases;
using InMemoryDatabase.UseCasePattern;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackReporting.Application.UseCases
{
    public class SearchFeedbackUseCase : UseCaseBase<FeedbackSearchRessource, IEnumerable<FeedbackRessource>>, ISearchFeedbackUseCase
    {
        private readonly IFeedbackRepository _feedbackRepo;
        private readonly IFeedbackKeywordsRepository _feedbackKeywordRepo;
        public override string ScopeKey => nameof(SearchFeedbackUseCase);

        public SearchFeedbackUseCase(ILogger<UseCaseBase<FeedbackSearchRessource, IEnumerable<FeedbackRessource>>> logger, IUnitOfWork unitOfWork, IFeedbackRepository feedbackRepo, IFeedbackKeywordsRepository feedbackKeywordRepo) : base(logger, unitOfWork)
        {
            _feedbackRepo = feedbackRepo;
            _feedbackKeywordRepo = feedbackKeywordRepo;
        }


        public override async Task<IEnumerable<FeedbackRessource>> ActuallyExecuteAsync(FeedbackSearchRessource searchQuery)
        {
            var feedbackIdsFilter = new List<int>();

            if (searchQuery.Keywords != null)
            {
                foreach (var keyword in searchQuery.Keywords)
                {
                    var feedbackKeywords = await _feedbackKeywordRepo.GetByKeywordHash(keyword.GetHashCode());

                    foreach (var feedbackKeyword in feedbackKeywords)
                        if (!feedbackIdsFilter.Contains(feedbackKeyword.FeedbackId))
                            feedbackIdsFilter.Add(feedbackKeyword.FeedbackId);
                }
            }

            var results = await _feedbackRepo.Search(searchQuery.CreatorName, searchQuery.DateOfCreation, feedbackIdsFilter);

            return results.Select(x => new FeedbackRessource(x));
        }
    }
}
