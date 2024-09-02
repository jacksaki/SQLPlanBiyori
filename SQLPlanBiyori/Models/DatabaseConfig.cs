using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SQLPlanBiyori.Models
{
    public class DatabaseConfig
    {
        [JsonPropertyName("connection_string")]
        public string ConnectionString
        {
            get; set;
        }

        [JsonPropertyName("temp_project_directory")]
        public string TempProjectDirectory
        {
            get; set;
        } = System.IO.Path.Combine(
                System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                "temp_project");

        [JsonPropertyName("dotnet_ef_name")]
        public string DotnetEFName
        {
            get; set;
        } = "dotnet-ef";

        [JsonPropertyName("model_directory")]
        public string ModelDir
        {
            get; set;
        } = "Models";

        [JsonPropertyName("db_context_name")]
        public string DbContextName
        {
            get; set;
        } = "AppDbContext";

        [JsonPropertyName("ef_design_name")]
        public string MicrosoftEntityFrameworkCoreDesignName
        {
            get; set;
        } = "Microsoft.EntityFrameworkCore.Design";

        [JsonPropertyName("npgsql_ef_name")]
        public string NpgsqlEntityFrameworkCoreName
        {
            get; set;
        } = "Npgsql.EntityFrameworkCore.PostgreSQL";
    }
}
