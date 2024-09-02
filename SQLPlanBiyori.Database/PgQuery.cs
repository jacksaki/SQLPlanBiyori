using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Npgsql;

namespace SQLPlanBiyori.Database;

public abstract class PgQuery : IDisposable
{

    protected bool disposedValue;
    public NpgsqlConnection Connection
    {
        get;
    }
    public PgQuery(string connectionString)
    {
        Connection = GenerateConnection(connectionString);
        Connection.Open();
    }
    protected abstract NpgsqlConnection GenerateConnection(string connectionString);

    public NpgsqlTransaction BeginTransaction()
    {
        return Connection.BeginTransaction();
    }
    protected virtual NpgsqlCommand GenerateCommand(string sql, IDictionary<string, object> param)
    {
        var cmd = Connection.CreateCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        if (param != null)
        {
            foreach (var p in param)
            {
                cmd.Parameters.Add(CreateParameter(p.Key, p.Value));
            }
        }
        return cmd;
    }
    protected abstract NpgsqlParameter CreateParameter(string name, object value);

    public SqlResultRow GetFirstRow(string sql, IDictionary<string, object> param)
    {
        return GetSqlResult(sql, param).Rows.FirstOrDefault();
    }

    public int ExecuteNonQuery(string sql, IDictionary<string, object> param)
    {
        return GenerateCommand(sql, param).ExecuteNonQuery();
    }

    public object ExecuteScalar(string sql, IDictionary<string, object> param)
    {
        return GenerateCommand(sql, param).ExecuteScalar();
    }

    public SqlResult GetSqlResult(string sql, IDictionary<string, object> param)
    {
        using var cmd = GenerateCommand(sql, param);
        using var dr = cmd.ExecuteReader();
        return new SqlResult(dr);
    }

    public async Task<SqlResult> GetSqlResultAsync(string sql, IDictionary<string, object> param)
    {
        using var cmd = GenerateCommand(sql, param);
        using var dr = await cmd.ExecuteReaderAsync();
        return await SqlResult.CreateAsync(dr);
    }


    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: マネージド状態を破棄します (マネージド オブジェクト)
            }
            try
            {
                Connection?.Close();
                Connection?.Dispose();
            }
            catch
            {
            }
            // TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、ファイナライザーをオーバーライドします
            // TODO: 大きなフィールドを null に設定します
            disposedValue = true;
        }
    }

    // // TODO: 'Dispose(bool disposing)' にアンマネージド リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします
    // ~OracleQuery()
    // {
    //     // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}