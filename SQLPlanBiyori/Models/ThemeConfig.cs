using MaterialDesignThemes.Wpf;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Windows.Media;

namespace SQLPlanBiyori.Models
{
    public class ThemeConfig
    {
        [JsonPropertyName("is_dark_theme")]
        public bool IsDarkTheme { get; set; }
        [JsonPropertyName("is_color_adjusted")]
        public bool IsColorAdjusted { get; set; }
        [JsonPropertyName("desired_contrast_ratio")]
        public float DesiredContrastRatio { get; set; }
        [JsonPropertyName("contrast_value")]
        public Contrast ContrastValue { get; set; }

        [JsonPropertyName("color_selection_value")]
        public ColorSelection ColorSelectionValue { get; set; }

        [JsonPropertyName("primay_color")]
        public Color PrimaryColor { get; set; }

        [JsonPropertyName("accent_color")]
        public Color AccentColor { get; set; }
    }
}
