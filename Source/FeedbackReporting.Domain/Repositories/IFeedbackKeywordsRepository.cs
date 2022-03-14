using FeedbackReporting.Domain.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeedbackReporting.Domain.Repositories
{
    public interface IFeedbackKeywordsRepository
    {
        Task<int> Insert(FeedbackKeywordEntity entityToInsert);
        Task<IEnumerable<FeedbackKeywordEntity>> GetByKeywordHash(int keywordHash);
    }
}
