using System.Text.Json.Serialization;

namespace MauiApp2026.Models
{
    public class Tag
    {
        public int ID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Color { get; set; } = "#FF0000";

        [JsonIgnore]
        public bool IsSelected { get; set; }
    }
}