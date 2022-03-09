using System.Threading.Tasks;

namespace InMemoryDatabase
{
    internal sealed class DefaultRepository : BaseRepository, IDefaultRepository
    {
        public DefaultRepository(IInMemoryDatabaseConnectionFactory connectionFactory) : base(connectionFactory)
        { }

        async Task<int> IDefaultRepository.ExecuteAsync(string sql)
        {
            return await base.ExecuteAsync(sql, null, null, null, null);
        }
    }
}
