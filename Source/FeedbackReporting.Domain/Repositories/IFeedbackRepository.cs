using FeedbackReporting.Domain.Models.Entities;
using System.Threading.Tasks;

namespace FeedbackReporting.Domain.Repositories
{
    public interface IFeedbackRepository
    {
        Task<int> Insert(FeedbackEntity entityToInsert);
        Task<FeedbackEntity> GetById(int id);
    }
}
