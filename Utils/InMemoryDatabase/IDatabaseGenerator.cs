using System;
using System.Threading.Tasks;

namespace InMemoryDatabase
{
    public interface IDatabaseGenerator
    {
        bool RegisterTableEntity(Type entityType);
        Task GenerateDatabaseAsync();
    }
}
