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

        public AttachDocumentToFeedbackUseCase(ILogger<AttachDocumentToFeedbackUseCase> logger, IUnitOfWork unitOfWork, IFeedbackRepository feedbackRepo) : base(logger, unitOfWork)
        {
            _feedbackRepo = feedbackRepo;
        }

        public override async Task<int> ActuallyExecuteAsync(FeedbackAttachmentRessource entityToInsert)
        {
            var feedback = await _feedbackRepo.GetById(entityToInsert.FeedbackId);

            if (feedback == null)
                throw new InvalidOperationException($"Unable to find feedbackId {entityToInsert.FeedbackId}");

            return await _feedbackAttachmentRepo.Insert(entityToInsert.ToEntity());
        }
    }
}
