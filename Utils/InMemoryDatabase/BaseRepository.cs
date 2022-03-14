using Dapper;
using Dapper.FastCrud;
using Dapper.FastCrud.Configuration.StatementOptions.Builders;
using InMemoryDatabase.UseCasePattern;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace InMemoryDatabase
{
    public class BaseRepository : IDisposable
    {        private bool _disposed = false;

        internal IUnitOfWork _unitOfWork;

        protected BaseRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        internal async Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _unitOfWork.Connection.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
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
                _unitOfWork?.Dispose();
            }
            _disposed = true;
        }
    }

    public class BaseRepository<TEntity> : BaseRepository
    {
        protected Type _entityType = typeof(TEntity);

        protected BaseRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }

        public async Task InsertAsync(TEntity entity)
        {
            await _unitOfWork.Connection.InsertAsync(entity, AttachTransaction);
        }

        public async Task<TEntity> GetAsync(TEntity entity)
        {
            return await _unitOfWork.Connection.GetAsync(entity, AttachTransaction);
        }

        public async Task<IEnumerable<TEntity>> FindAsync(FormattableString whereClause, object parameters)
        {
            if (_unitOfWork.Transaction != null)
            {
                return await _unitOfWork.Connection.FindAsync<TEntity>(x => x.Where(whereClause).WithParameters(parameters).AttachToTransaction(_unitOfWork.Transaction));
            }
            return await _unitOfWork.Connection.FindAsync<TEntity>(x => x.Where(whereClause).WithParameters(parameters));
        }

        public async Task<bool> DeleteAsync(TEntity entityToDelete)
        {
            return await _unitOfWork.Connection.DeleteAsync(entityToDelete, AttachTransaction);
        }

        private void AttachTransaction(IStandardSqlStatementOptionsBuilder<TEntity> statement)
        {
            if (_unitOfWork.Transaction != null)
            {
                statement.AttachToTransaction(_unitOfWork.Transaction);
            }
        }

        private void AttachTransaction(IRangedBatchSelectSqlSqlStatementOptionsOptionsBuilder<TEntity> statement)
        {
            if (_unitOfWork.Transaction != null)
            {
                statement.AttachToTransaction(_unitOfWork.Transaction);
            }
        }

        private void AttachTransaction(ISelectSqlSqlStatementOptionsBuilder<TEntity> statement)
        {
            if (_unitOfWork.Transaction != null)
            {
                statement.AttachToTransaction(_unitOfWork.Transaction);
            }
        }
    }
}
