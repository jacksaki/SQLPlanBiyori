using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace SQLPlanBiyori.Database;

public class SqlResult
{
    public SqlResult(NpgsqlDataReader dr)
    {
        Columns = new SqlResultColumnCollection(this, dr);
        Rows = new SqlResultRowCollection(this,dr);
    }

    public static async Task<SqlResult> CreateAsync(NpgsqlDataReader dr)
    {
        var result = new SqlResult(dr);
        result.Columns = new SqlResultColumnCollection(result, dr);
        result.Rows = await SqlResultRowCollection.CreateAsync(result,dr);
        return result;
    }

    public SqlResultColumnCollection Columns
    {
        get;
        private set;
    }
    public SqlResultRowCollection Rows
    {
        get;
        private set;
    }
}
