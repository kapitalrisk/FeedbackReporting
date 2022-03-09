using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace InMemoryDatabase
{
    public static class DbEntitiesExtensions
    {
        public static bool HaveTableAttribute(this Type entityType)
        {
            if (entityType == null || !entityType.IsClass)
                return false;

            return entityType.GetCustomAttribute<TableAttribute>() != null;
        }

        public static bool HavePropertyWithColumnAttribute(this Type entityType)
        {
            if (entityType == null || !entityType.IsClass)
                return false;

            return entityType.GetProperties().Any(x => x.GetCustomAttribute<ColumnAttribute>() != null);
        }

        public static bool IsPrimaryKey(this PropertyInfo entityPropertyInfo) => entityPropertyInfo?.GetCustomAttribute<KeyAttribute>() != null;

        public static Dictionary<string, PropertyInfo> GetColumnProperties(this Type entityType)
        {
            var result = new Dictionary<string, PropertyInfo>();

            if (entityType == null || !entityType.IsClass)
                return result;

            foreach (var property in entityType.GetProperties())
            {
                var columnAttr = property.GetCustomAttribute<ColumnAttribute>();

                if (columnAttr != null)
                    result.Add(columnAttr.Name, property);
            }

            return result;
        }

        public static string GetColumnName(this Type entityType, string propertyName, string alias = null)
        {
            if (entityType == null || String.IsNullOrWhiteSpace(propertyName) || !entityType.IsClass)
                return null;

            var columnName = entityType.GetProperty(propertyName)?.GetCustomAttribute<ColumnAttribute>()?.Name;

            if (String.IsNullOrWhiteSpace(columnName))
                return null;

            return String.IsNullOrWhiteSpace(alias) ? columnName : $"{alias}.{columnName}";
        }

        public static string GetColumnNameList(this Type entityType, string alias = null)
        {
            if (entityType == null || !entityType.IsClass)
                return null;

            var columnNamesList = new List<string>();

            foreach (var columnProp in entityType.GetColumnProperties())
            {
                var columnName = columnProp.Key;

                if (!String.IsNullOrWhiteSpace(columnName) && !String.IsNullOrWhiteSpace(alias))
                    columnName = $"{alias}.{columnName}";
                if (!String.IsNullOrWhiteSpace(columnName))
                    columnNamesList.Add(columnName);
            }

            return columnNamesList.Count == 0 ? null : String.Join(", ", columnNamesList);
        }

        public static string GetTableName(this Type entityType, string alias = null)
        {
            if (entityType == null || !entityType.IsClass)
                return null;
            var tableAttr = entityType.GetCustomAttribute<TableAttribute>();

            return tableAttr != null ? $"{tableAttr.Name}{(alias != null ? " as " + alias : "")}" : null;
        }

        public static string GetSchemaName(this Type entityType)
        {
            return entityType == null || !entityType.IsClass ? null : entityType.GetCustomAttribute<TableAttribute>()?.Schema;
        }

        public static string GetSchemaAndTableName(this Type entityType, string alias = null)
        {
            if (entityType == null || !entityType.IsClass)
                return null;
            var tableAttr = entityType.GetCustomAttribute<TableAttribute>();

            return tableAttr == null ? null : String.IsNullOrWhiteSpace(tableAttr.Schema) ? tableAttr.Name : $"{tableAttr.Schema}.{tableAttr.Name}{(alias != null ? " as " + alias : "")}";
            
        }

        public static SqliteType ToSqliteType(this Type propertyType)
        {
            if (propertyType == null)
                return SqliteType.unknown;

            if (propertyType == typeof(Boolean) ||
                propertyType == typeof(Byte) ||
                propertyType == typeof(SByte) ||
                propertyType == typeof(Int16) ||
                propertyType == typeof(Int32) ||
                propertyType == typeof(Int64) ||
                propertyType == typeof(UInt16) ||
                propertyType == typeof(UInt32) ||
                propertyType == typeof(UInt64)) // may overflow
                return SqliteType.integer;

            if (propertyType == typeof(Char) ||
                propertyType == typeof(DateTime) ||
                propertyType == typeof(DateTimeOffset) ||
                propertyType == typeof(Decimal) || // SqliteType.real would be lossy
                propertyType == typeof(Guid) ||
                propertyType == typeof(String) ||
                propertyType == typeof(TimeSpan))
                return SqliteType.text;

            if (propertyType == typeof(Double) ||
                propertyType == typeof(Single))
                return SqliteType.real;

            if (propertyType == typeof(Byte[]))
                return SqliteType.blob;

            return SqliteType.unknown; // we may want to let this default case be parametrable

        }
    }
}
