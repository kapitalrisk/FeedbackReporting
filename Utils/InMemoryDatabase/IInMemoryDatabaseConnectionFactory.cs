using System;
using System.Data;

namespace InMemoryDatabase
{
    public interface IInMemoryDatabaseConnectionFactory : IDisposable
    {
        IDbConnection Create();
    }
}
