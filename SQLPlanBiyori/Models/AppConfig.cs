using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Utf8Json;
using Utf8Json.Resolvers;

namespace SQLPlanBiyori.Models
{
    public class AppConfig
    {
        public static string Path => System.IO.Path.ChangeExtension(System.Reflection.Assembly.GetExecutingAssembly().Location, ".conf");

        [JsonPropertyName("database")]
        public DatabaseConfig DatabaseConfig { get; protected set; }

        [JsonPropertyName("theme")]
        public ThemeConfig ThemeConfig { get; protected set; }

        public static AppConfig Load()
        {
            if (!System.IO.File.Exists(Path))
            {
                return new AppConfig()
                {
                    ThemeConfig = new ThemeConfig()
                };
            }
            var resolver = StandardResolver.AllowPrivate;
            return JsonSerializer.Deserialize<AppConfig>(System.IO.File.ReadAllBytes(Path), resolver);
        }
        public void Save()
        {
            System.IO.File.WriteAllBytes(Path, JsonSerializer.Serialize<AppConfig>(this, StandardResolver.AllowPrivate));
        }
    }
}
