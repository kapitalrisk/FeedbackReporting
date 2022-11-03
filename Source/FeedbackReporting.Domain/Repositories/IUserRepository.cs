using FeedbackReporting.Domain.Models.Entities;
using System.Threading.Tasks;

namespace FeedbackReporting.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<int> Insert(UserEntity entityToInsert);
        Task<UserEntity> GetByName(string name);
        Task<bool> DeleteByName(string name);
    }
}
