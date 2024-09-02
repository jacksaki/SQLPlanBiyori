using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Npgsql;

namespace SQLPlanBiyori.Database
{
    public class SqlResultColumnCollection : IEnumerable<SqlResultColumn>
    {
        private readonly Dictionary<string, SqlResultColumn> _columns = null;
        public SqlResultColumnCollection(SqlResult parent, NpgsqlDataReader dr)
        {
            Parent = parent;
            Count = dr.FieldCount;
            _columns = new Dictionary<string, SqlResultColumn>(StringComparer.OrdinalIgnoreCase);
            for (var i = 0; i < Count; i++)
            {
                _columns.Add(dr.GetName(i), new SqlResultColumn(dr, i, this));
            }
        }

        public SqlResultColumn this[string name]
        {
            get
            {
                return this._columns[name];
            }
        }

        public int Count
        {
            get;
        }
        public SqlResult Parent
        {
            get;
        }

        public IEnumerator<SqlResultColumn> GetEnumerator()
        {
            return ((IEnumerable<SqlResultColumn>)_columns).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_columns).GetEnumerator();
        }
    }
}
