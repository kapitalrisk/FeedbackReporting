using FeedbackReporting.Domain.Models.Entities;
using FeedbackReporting.Domain.Repositories;
using InMemoryDatabase;
using InMemoryDatabase.UseCasePattern;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeedbackReporting.Application.Repositories
{
    public class FeedbackKeywordsRepository : BaseRepository<FeedbackKeywordEntity>, IFeedbackKeywordsRepository
    {
        public FeedbackKeywordsRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }

        public async Task<int> Insert(FeedbackKeywordEntity entityToInsert)
        {
            await base.InsertAsync(entityToInsert);
            return entityToInsert.FeedbackId;
        }

        public Task<IEnumerable<FeedbackKeywordEntity>> GetByKeywordHash(int keywordHash)
        {
            var keywordHashColumn = _entityType.GetColumnName(nameof(FeedbackKeywordEntity.HashCode));
            var result = base.FindAsync($"{keywordHashColumn} = @keywordHashParam", new { keywordHashParam = keywordHash });

            return result;
        }
    }
}
