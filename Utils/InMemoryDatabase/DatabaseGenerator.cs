using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InMemoryDatabase
{
    public class DatabaseGenerator : IDatabaseGenerator
    {
        private readonly IDefaultRepository _defaultRepository;
        private readonly List<Type> _tableEntities = new List<Type>();

        public DatabaseGenerator(IDefaultRepository defaultRepository)
        {
            _defaultRepository = defaultRepository;
        }

        public bool RegisterTableEntity(Type entityType)
        {
            if (entityType == null)
                throw new ArgumentNullException("Provided EntityType must not be null", nameof(entityType));
            if (!entityType.HaveTableAttribute())
                throw new ArgumentException("Provided EntityType must be a class with a TableAttribute (from namespace System.ComponentModel.DataAnnotations.Schema)", nameof(entityType));
            if (!entityType.HavePropertyWithColumnAttribute())
                throw new ArgumentException("Provided EntityType must be a class with at least one property with a ColumnAttribute (from namespace System.ComponentModel.DataAnnotations.Schema)", nameof(entityType));

            if (_tableEntities.Contains(entityType))
                return false;

            _tableEntities.Add(entityType);
            return true;
        }

        public async Task GenerateDatabaseAsync()
        {
            await _defaultRepository.ExecuteAsync(GenerateCreateTablesQuery());
        }

        private string GenerateCreateTablesQuery()
        {
            var result = new List<string>();

            foreach (var entityType in _tableEntities)
                result.Add(GenerateCreateTableQuery(entityType));

            return String.Join(" ", result);
        }

        private string GenerateCreateTableQuery(Type entityType) => new CreateTableScriptBuilder().WithEntityType(entityType).Build();
    }
}
