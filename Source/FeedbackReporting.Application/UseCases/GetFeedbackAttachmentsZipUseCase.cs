using FeedbackReporting.Domain.Repositories;
using FeedbackReporting.Domain.UseCases;
using InMemoryDatabase.UseCasePattern;
using Microsoft.Extensions.Logging;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackReporting.Application.UseCases
{
    public class GetFeedbackAttachmentsZipUseCase : UseCaseBase<int, MemoryStream>, IGetFeedbackAttachmentsZipUseCase
    {
        private readonly IFeedbackAttachmentRepository _feedbackAttachmentRepo;

        public GetFeedbackAttachmentsZipUseCase(ILogger<UseCaseBase<int, MemoryStream>> logger, IUnitOfWork unitOfWork, IFeedbackAttachmentRepository feedbackAttachmentRepo) : base(logger, unitOfWork)
        {
            _feedbackAttachmentRepo = feedbackAttachmentRepo;
        }

        public override string ScopeKey => nameof(GetFeedbackAttachmentsZipUseCase);

        public override async Task<MemoryStream> ActuallyExecuteAsync(int feedbackId)
        {
            var attachments = await _feedbackAttachmentRepo.GetAttachmentsByFeedbackId(feedbackId);
            using var resultMemoryStream = new MemoryStream();
            using var zipArchive = new ZipArchive(resultMemoryStream, ZipArchiveMode.Create);

            foreach (var attachment in attachments)
            {
                var file = zipArchive.CreateEntry(attachment.FileName);
                using var streamWriter = new StreamWriter(file.Open());
                streamWriter.Write(Encoding.UTF8.GetString(attachment.Data));
            }

            return resultMemoryStream;
        }
    }
}
