using FeedbackReporting.Domain.Models.Ressources;
using FeedbackReporting.Domain.Repositories;
using FeedbackReporting.Domain.UseCases;
using InMemoryDatabase.UseCasePattern;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FeedbackReporting.Application.UseCases
{
    public class AttachDocumentToFeedbackUseCase : UseCaseBase<FeedbackAttachmentRessource, int>, IAttachDocumentToFeedbackUseCase
    {
        private readonly IFeedbackRepository _feedbackRepo;
        private readonly IFeedbackAttachmentRepository _feedbackAttachmentRepo;
        public override string ScopeKey => nameof(AttachDocumentToFeedbackUseCase);

        public AttachDocumentToFeedbackUseCase(ILogger<AttachDocumentToFeedbackUseCase> logger, IUnitOfWork unitOfWork, IFeedbackRepository feedbackRepo, IFeedbackAttachmentRepository feedbackAttachmentRepo) : base(logger, unitOfWork)
        {
            _feedbackRepo = feedbackRepo;
            _feedbackAttachmentRepo = feedbackAttachmentRepo;
        }

        public override async Task<int> ActuallyExecuteAsync(FeedbackAttachmentRessource entityToInsert)
        {
            var feedback = await _feedbackRepo.GetById(entityToInsert.FeedbackId);

            if (feedback == null)
                throw new InvalidOperationException($"Unable to find feedbackId {entityToInsert.FeedbackId}");

            var alreadyExistingAttachment = await _feedbackAttachmentRepo.GetAttachmentsByFeedbackIdAndFileName(entityToInsert.FeedbackId, entityToInsert.FileName);

            if (alreadyExistingAttachment != null)
                throw new InvalidOperationException("Unable to add attachment : a file with the same name already exists for the requested feedback id");

            return await _feedbackAttachmentRepo.Insert(entityToInsert.ToEntity());
        }
    }
}
