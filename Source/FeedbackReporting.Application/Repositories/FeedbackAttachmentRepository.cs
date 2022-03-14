using FeedbackReporting.Domain.Models.Entities;
using FeedbackReporting.Domain.Repositories;
using InMemoryDatabase;
using InMemoryDatabase.UseCasePattern;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackReporting.Application.Repositories
{
    class FeedbackAttachmentRepository : BaseRepository<FeedbackAttachmentEntity>, IFeedbackAttachmentRepository
    {
        public FeedbackAttachmentRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }

        public async Task<int> Insert(FeedbackAttachmentEntity entityToInsert)
        {
            await base.InsertAsync(entityToInsert);
            return entityToInsert.Id;
        }

        public async Task<IEnumerable<FeedbackAttachmentEntity>> GetAttachmentsByFeedbackId(int feedbackId)
        {
            var feedbackIdColumn = _entityType.GetColumnName(nameof(FeedbackAttachmentEntity.FeedbackId));
            var result = await base.FindAsync($"{feedbackIdColumn} = @feedbackIdParam", new { feedbackIdParam = feedbackId });

            return result;
        }

        public async Task<FeedbackAttachmentEntity> GetAttachmentsByFeedbackIdAndFileName(int feedbackId, string fileName)
        {
            var feedbackIdColumn = _entityType.GetColumnName(nameof(FeedbackAttachmentEntity.FeedbackId));
            var feedbackFileNameColumn = _entityType.GetColumnName(nameof(FeedbackAttachmentEntity.FileName));
            var result = await base.FindAsync($"{feedbackIdColumn} = @feedbackIdParam and {feedbackFileNameColumn} = @feedbackFileNameParam", new { feedbackIdParam = feedbackId, feedbackFileNameParam = fileName });

            return result != null && result.Count() == 1 ? result.First() : null;
        }
    }
}
