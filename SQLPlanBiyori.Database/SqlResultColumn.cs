using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SQLPlanBiyori.Database
{
    public class SqlResultColumn
    {
        internal SqlResultColumn(IDataReader dr, int index, SqlResultColumnCollection parent)
        {
            ColumnName = dr.GetName(index);
            ColumnIndex = index;
            DataType = dr.GetFieldType(index);
            Parent = parent;
        }

        public SqlResultColumnCollection Parent
        {
            get;
        }

        public string ColumnName
        {
            get;
        }

        public int ColumnIndex
        {
            get;
        }

        public Type DataType
        {
            get;
        }
    }
}
