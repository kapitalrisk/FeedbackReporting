using InMemoryDatabase.UseCasePattern;
using System.IO;

namespace FeedbackReporting.Domain.UseCases
{
    public interface IGetFeedbackAttachmentsZipUseCase : IUseCase<int, MemoryStream>
    { }
}
