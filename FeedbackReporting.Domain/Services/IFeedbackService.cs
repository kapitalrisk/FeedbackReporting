using FeedbackReporting.Domain.Models.Ressources;
using System.Threading.Tasks;

namespace FeedbackReporting.Domain.Services
{
    public interface IFeedbackService
    {
        Task<int> Insert(FeedbackRessource entityToInsert);
        Task<FeedbackRessource> GetById(int id);
    }
}
