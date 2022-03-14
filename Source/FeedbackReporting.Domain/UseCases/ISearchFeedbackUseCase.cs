using FeedbackReporting.Domain.Models.Ressources;
using InMemoryDatabase.UseCasePattern;
using System.Collections.Generic;

namespace FeedbackReporting.Domain.UseCases
{
    public interface ISearchFeedbackUseCase : IUseCase<FeedbackSearchRessource, IEnumerable<FeedbackRessource>>
    { }
}
