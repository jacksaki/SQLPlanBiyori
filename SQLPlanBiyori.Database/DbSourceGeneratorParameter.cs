using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLPlanBiyori.Database;
public class DbSourceGeneratorParameter
{
    public string ConnectionString
    {
        get; set;
    }

    public string TempProjectDirectory
    {
        get; set;
    } = System.IO.Path.Combine(
            System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), 
            "temp_project");

    public string DotnetEFName
    {
        get; set;
    } = "dotnet-ef";

    public string ModelDir
    {
        get; set;
    } = "Models";

    public string DbContextName
    {
        get; set;
    } = "AppDbContext";

    public string MicrosoftEntityFrameworkCoreDesignName
    {
        get; set;
    } = "Microsoft.EntityFrameworkCore.Design";

    public string NpgsqlEntityFrameworkCoreName
    {
        get; set;
    } = "Npgsql.EntityFrameworkCore.PostgreSQL";
}
