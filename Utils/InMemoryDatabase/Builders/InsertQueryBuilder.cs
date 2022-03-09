using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace InMemoryDatabase.Builders
{
    public class InsertQueryBuilder
    {
        private dynamic _entity;
        private Type _entityType;
        private IDbTransaction _transaction;
        private const string INSERT_KEYWORD = "insert";
        private const string INTO_KEYWORD = "into";
        private const string VALUES_KEYWORD = "values";

        public string Build()
        {
            var columnsProperties = _entityType.GetColumnProperties();

            var sb = new StringBuilder();
            sb.AppendLine(INSERT_KEYWORD);
            sb.AppendLine(INTO_KEYWORD);
            sb.AppendLine(_entityType.GetTableName());
            sb.AppendLine("(");
            sb.AppendLine(string.Join(", ", columnsProperties.Keys));
            sb.AppendLine(")");
            sb.AppendLine(VALUES_KEYWORD);
            sb.AppendLine("(");

            var columnValues = new List<string>();

            foreach (var prop in columnsProperties)
            {
                var sqliteType = prop.Value.PropertyType.ToSqliteType();

                if (sqliteType == SqliteType.integer || sqliteType == SqliteType.real)
                    columnValues.Add($"{prop.Value.GetValue(_entity)}");
                else //if (sqliteType == SqliteType.text || sqliteType == SqliteType.blob)
                    columnValues.Add($"\"{prop.Value.GetValue(_entity)}\"");
            }
            sb.AppendLine(string.Join(", ", columnValues));
            sb.AppendLine(")");

            return sb.ToString();
        }

        public InsertQueryBuilder WithEntity(object entity)
        {
            _entityType = entity.GetType();
            _entity = entity;
            return this;
        }


    }
}
