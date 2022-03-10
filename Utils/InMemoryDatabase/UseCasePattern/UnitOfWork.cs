using System;
using System.Data;

namespace InMemoryDatabase.UseCasePattern
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private bool _disposed = false;

        public IDbConnection Connection { get; private set; }

        public IDbTransaction Transaction { get; private set; }

        public UnitOfWork(IInMemoryDatabaseConnectionFactory inMemoryDatabaseConnectionFactory)
        {
            Connection = inMemoryDatabaseConnectionFactory.Create();
            Connection.Open();
        }

        public void Begin()
        {
            Transaction = Connection.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public void Commit()
        {
            Transaction.Commit();
        }

        public void Rollback()
        {
            Transaction.Rollback();
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
                Transaction?.Dispose();
                Connection?.Dispose();
                Transaction = null;
                Connection = null;
            }
            _disposed = true;
        }
    }
}
