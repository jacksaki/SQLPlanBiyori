using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SQLPlanBiyori.Database
{
    public class SqlResultRow
    {
        public SqlResultRow(IDataReader dr, SqlResultRowCollection parent)
        {
            Parent = parent;
            Values = new object[parent.Parent.Columns.Count];
            dr.GetValues(Values);
        }

        public object this[int columnIndex]
        {
            get
            {
                return Values[columnIndex];
            }
        }

        public object this[string columnName]
        {
            get
            {
                return Values[this.Parent.Parent.Columns[columnName].ColumnIndex];
            }
        }

        public SqlResultRowCollection Parent
        {
            get;
        }

        public object[] Values
        {
            get;
        }
    }
}
