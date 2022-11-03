using FeedbackReporting.Domain.Models.Ressources;
using InMemoryDatabase.UseCasePattern;

namespace FeedbackReporting.Domain.UseCases
{
    public interface IGetFeedbackByIdUseCase : IUseCase<int, FeedbackRessource>
    { }
}
