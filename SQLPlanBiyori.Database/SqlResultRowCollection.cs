using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace SQLPlanBiyori.Database;

public class SqlResultRowCollection : IEnumerable<SqlResultRow>
{
    private readonly List<SqlResultRow> _rows = null;
    internal SqlResultRowCollection(SqlResult parent, NpgsqlDataReader dr)
        :this(parent)
    {
        while (dr.Read())
        {
            _rows.Add(new SqlResultRow(dr, this));
        }
    }

    internal static async Task<SqlResultRowCollection> CreateAsync(SqlResult parent, NpgsqlDataReader dr)
    {
        var rows = new SqlResultRowCollection(parent);
        while (await dr.ReadAsync())
        {
            rows._rows.Add(new SqlResultRow(dr, rows));
        }
        return rows;
    }

    private SqlResultRowCollection(SqlResult parent)
    {
        this.Parent = parent;
        _rows = new List<SqlResultRow>();
    }

    public SqlResult Parent
    {
        get;
    }


    public IEnumerator<SqlResultRow> GetEnumerator()
    {
        return ((IEnumerable<SqlResultRow>)_rows).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_rows).GetEnumerator();
    }
}
