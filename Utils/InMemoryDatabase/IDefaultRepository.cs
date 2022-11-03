using System.Threading.Tasks;

namespace InMemoryDatabase
{
    public interface IDefaultRepository
    {
        Task<int> ExecuteAsync(string sql);
    }
}
