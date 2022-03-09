using Dapper.FastCrud;
using Microsoft.Data.Sqlite;
using System;
using System.Data;

namespace InMemoryDatabase
{
    internal sealed class InMemoryDatabaseConnectionFactory : IInMemoryDatabaseConnectionFactory
    {
        // When using in memory database always keep an open connection during application lifecycle
        private readonly IDbConnection _masterConnection;

        // Using a name and a shared cache allows multiple connections to access the same in-memory database
        private readonly string _connectionString = "Data Source=InMemorySample;Mode=Memory;Cache=Shared";
        private bool _disposed = false;

        public InMemoryDatabaseConnectionFactory()
        {
            OrmConfiguration.DefaultDialect = SqlDialect.SqLite;
            _masterConnection = this.Create();
        }

        public IDbConnection Create()
        {
            var dbConnection = new SqliteConnection(_connectionString);
            dbConnection.Open();
            return dbConnection;
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
                _masterConnection?.Dispose();
            }
            _disposed = true;
        }
    }
}
