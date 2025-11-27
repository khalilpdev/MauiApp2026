using Avalonia;
using Avalonia.Media;
using System.Text.Json.Serialization;

namespace MauiApp2026.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Color { get; set; } = "#FF0000";

        [JsonIgnore]
        public Brush ColorBrush
        {
            get
            {
                try
                {
                    return new SolidColorBrush(Avalonia.Media.Color.Parse(Color));
                }
                catch
                {
                    return new SolidColorBrush(Colors.Red);
                }
            }
        }

        public override string ToString() => $"{Title}";
    }
}