using Dapper;
using Dapper.FastCrud;
using System;
using System.Data;
using System.Threading.Tasks;

namespace InMemoryDatabase
{
    public class BaseRepository : IDisposable
    {
        private readonly IInMemoryDatabaseConnectionFactory _connectionFactory;

        private bool _disposed = false;

        internal IDbConnection Connection => _connectionFactory.Create();

        protected BaseRepository(IInMemoryDatabaseConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        internal async Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await Connection.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                Connection?.Close();
            }
            _disposed = true;
        }
    }

    public class BaseRepository<TEntity> : BaseRepository
    {
        protected BaseRepository(IInMemoryDatabaseConnectionFactory connectionFactory) : base (connectionFactory)
        { }

        public async Task InsertAsync(TEntity entity)
        {
            await Connection.InsertAsync(entity);
        }

        public async Task<TEntity> GetAsync(TEntity entity)
        {
            return await Connection.GetAsync(entity);
        }
    }
}
