using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace SQLPlanBiyori.Extensions
{
    public class SqlResultColumnCollection : IReadOnlyDictionary<string, SqlResultColumn>
    {
        public int Count { get; }
        public SqlResult Parent { get; }

        public IEnumerable<string> Keys => ((IReadOnlyDictionary<string, SqlResultColumn>)_columns).Keys;

        public IEnumerable<SqlResultColumn> Values => ((IReadOnlyDictionary<string, SqlResultColumn>)_columns).Values;

        public SqlResultColumn this[string key] => ((IReadOnlyDictionary<string, SqlResultColumn>)_columns)[key];

        private Dictionary<string, SqlResultColumn> _columns = null;
        public SqlResultColumnCollection(IDataReader dr, SqlResult parent)
        {
            this.Parent = parent;
            this.Count = dr.FieldCount;
            _columns = Enumerable.Range(0, dr.FieldCount).
                ToDictionary(x => dr.GetName(x), y => new SqlResultColumn(dr, y, this),StringComparer.OrdinalIgnoreCase);
        }

        public bool ContainsKey(string key)
        {
            return ((IReadOnlyDictionary<string, SqlResultColumn>)_columns).ContainsKey(key);
        }

        public bool TryGetValue(string key, [MaybeNullWhen(false)] out SqlResultColumn value)
        {
            return ((IReadOnlyDictionary<string, SqlResultColumn>)_columns).TryGetValue(key, out value);
        }

        public IEnumerator<KeyValuePair<string, SqlResultColumn>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<string, SqlResultColumn>>)_columns).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_columns).GetEnumerator();
        }
    }
}