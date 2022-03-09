using System;
using System.Collections.Generic;

namespace InMemoryDatabase
{
    internal class CreateTableScriptBuilder
    {
        private static Type _entityType = null;

        public string Build()
        {
            var query = $"create table if not exists {_entityType.GetTableName()} "; // sqlite does not handle schemas
            query += "( ";
            var columnsDefinitions = new List<string>();
            
            foreach (var columnDef in _entityType.GetColumnProperties())
                columnsDefinitions.Add($"{columnDef.Key} {columnDef.Value.PropertyType.ToSqliteType()}{(columnDef.Value.IsPrimaryKey() ?  " primary key" : "")}");

            query += String.Join(", ", columnsDefinitions);

            query += " );";
            return query;
        }

        public CreateTableScriptBuilder WithEntityType(Type entityType)
        {
            _entityType = entityType;
            return this;
        }
    }
}
