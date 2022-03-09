using FeedbackReporting.Domain.Models.Ressources;
using FeedbackReporting.Domain.Repositories;
using FeedbackReporting.Domain.Services;
using System.Threading.Tasks;

namespace FeedbackReporting.Application.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepo;

        public FeedbackService(IFeedbackRepository feedbackRepo)
        {
            _feedbackRepo = feedbackRepo;
        }

        public async Task<int> Insert(FeedbackRessource entityToInsert)
        {
            return await _feedbackRepo.Insert(entityToInsert.ToEntity());
        }

        public async Task<FeedbackRessource> GetById(int id)
        {
            var result = await _feedbackRepo.GetById(id);
            return result != null ? new FeedbackRessource(result) : null;
        }
    }
}
