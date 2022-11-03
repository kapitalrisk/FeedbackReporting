using FeedbackReporting.Domain.Models.Entities;
using FeedbackReporting.Domain.Repositories;
using InMemoryDatabase;
using InMemoryDatabase.UseCasePattern;
using System.Threading.Tasks;

namespace FeedbackReporting.Application.Repositories
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }

        public async Task<UserEntity> GetByName(string name)
        {
            return await base.GetAsync(new UserEntity { Name = name });
        }

        public async Task<int> Insert(UserEntity entityToInsert)
        {
            await base.InsertAsync(entityToInsert);
            return entityToInsert.Id;
        }

        public async Task<bool> DeleteByName(string name)
        {
            return await base.DeleteAsync(new UserEntity { Name = name });
        }
    }
}
