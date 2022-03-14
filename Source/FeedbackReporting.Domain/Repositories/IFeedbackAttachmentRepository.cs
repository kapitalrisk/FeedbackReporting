using FeedbackReporting.Domain.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeedbackReporting.Domain.Repositories
{
    public interface IFeedbackAttachmentRepository
    {
        Task<int> Insert(FeedbackAttachmentEntity entityToInsert);
        Task<IEnumerable<FeedbackAttachmentEntity>> GetAttachmentsByFeedbackId(int feedbackId);
    }
}
