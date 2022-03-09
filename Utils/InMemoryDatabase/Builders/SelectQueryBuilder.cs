using System;
using System.Text;

namespace InMemoryDatabase.Builders
{
    public class SelectQueryBuilder
    {
        private Type _entityType;
        private const string SELECT_KEYWORD = "select";
        private const string FROM_KEYWORD = "from";

        public string Build()
        {
            var sb = new StringBuilder();

            sb.AppendLine(SELECT_KEYWORD);
            sb.AppendLine(_entityType.GetColumnNameList());
            sb.AppendLine(FROM_KEYWORD);
            sb.AppendLine(_entityType.GetTableName());

            return sb.ToString();
        }

        public SelectQueryBuilder WithEntityType(Type entityType)
        {
            _entityType = entityType;
            return this;
        }
    }
}
