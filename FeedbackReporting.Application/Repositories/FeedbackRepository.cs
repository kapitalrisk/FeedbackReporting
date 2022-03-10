using FeedbackReporting.Domain.Models.Entities;
using FeedbackReporting.Domain.Repositories;
using InMemoryDatabase;
using InMemoryDatabase.UseCasePattern;
using System.Threading.Tasks;

namespace FeedbackReporting.Application.Repositories
{
    public class FeedbackRepository : BaseRepository<FeedbackEntity>, IFeedbackRepository
    {
        public FeedbackRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }

        public async Task<int> Insert(FeedbackEntity entityToInsert)
        {
            await base.InsertAsync(entityToInsert);
            return entityToInsert.Id;
        }

        public async Task<FeedbackEntity> GetById(int id)
        {
            return await base.GetAsync(new FeedbackEntity { Id = id });
        }
    }
}
