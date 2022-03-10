using FeedbackReporting.Domain.Models.Entities;
using FeedbackReporting.Domain.Repositories;
using InMemoryDatabase;
using InMemoryDatabase.UseCasePattern;
using System.Collections.Generic;
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

        public Task<IEnumerable<FeedbackAttachmentEntity>> GetAttachmentsByFeedbackId(int feedbackId)
        {
            var feedbackIdColumn = typeof(FeedbackAttachmentEntity).GetColumnName(nameof(FeedbackAttachmentEntity.FeedbackId));
            var result = base.FindAsync($"{feedbackIdColumn} = @feedbackIdParam", new { feedbackIdParam = feedbackId });

            return result;
        }
    }
}
