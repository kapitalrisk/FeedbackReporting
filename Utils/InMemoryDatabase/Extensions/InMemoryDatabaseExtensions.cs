using InMemoryDatabase.Builders;
using System.Collections.Generic;
using System.Data;

namespace InMemoryDatabase.Extensions
{
    public static class InMemoryDatabaseExtensions
    {
        public static void Insert<TEntity>(this IDbConnection connection, TEntity entityToInsert)
        {
            var insertQueryStr = new InsertQueryBuilder()
                .WithEntity(entityToInsert)
                .Build();

            var sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = insertQueryStr;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.ExecuteNonQuery();
        }
    }
}
