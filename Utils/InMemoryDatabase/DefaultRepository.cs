using Dapper;
using System.Threading.Tasks;

namespace InMemoryDatabase
{
    internal sealed class DefaultRepository : IDefaultRepository
    {
        private readonly IInMemoryDatabaseConnectionFactory _connectionFactory;

        public DefaultRepository(IInMemoryDatabaseConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        async Task<int> IDefaultRepository.ExecuteAsync(string sql)
        {
            return await _connectionFactory.Create().ExecuteAsync(sql, null, null, null, null);
        }
    }
}
