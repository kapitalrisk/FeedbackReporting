using FeedbackReporting.Domain.Models.Entities;
using FeedbackReporting.Domain.Repositories;
using InMemoryDatabase;
using System.Threading.Tasks;

namespace FeedbackReporting.Application.Repositories
{
    public class FeedbackRepository : BaseRepository<FeedbackEntity>, IFeedbackRepository
    {
        public FeedbackRepository(IInMemoryDatabaseConnectionFactory connectionFactory) : base(connectionFactory)
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
