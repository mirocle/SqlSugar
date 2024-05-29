using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace SqlSugar
{
    public class MySqlBuilder : SqlBuilderProvider
    {
        public override string SqlTranslationLeft { get { return "`"; } }
        public override string SqlTranslationRight { get { return "`"; } }
        public override string SqlDateNow
        {
            get
            {
                return "NOW(6)";
            }
        }
        public override string FullSqlDateNow
        {
            get
            {
                return "select NOW(6)";
            }
        }
        public override string GetUnionFomatSql(string sql)
        {
            return " ( " + sql + " )  ";
        }

        // add by victor
        public override string GetTranslationTableName(string name)
        {
            Check.ArgumentNullException(name, string.Format(ErrorMessage.ObjNotExist, "Table Name"));
            if (!name.Contains("<>f__AnonymousType") && name.IsContainsIn("(", ")", SqlTranslationLeft) && name != "Dictionary`2")
            {
                return name;
            }
            var context = this.Context;
            var mappingInfo = context
                .MappingTables
                .FirstOrDefault(it => it.EntityName.Equals(name, StringComparison.CurrentCultureIgnoreCase));
            name = (mappingInfo == null ? name : mappingInfo.DbTableName);
            if (name.IsContainsIn("(", ")", SqlTranslationLeft))
            {
                return name;
            }

            return SqlTranslationLeft + name + SqlTranslationRight;
        }
    }
}
