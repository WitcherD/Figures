using System.Text.Json.Serialization;

namespace Figures.Contracts.Dto
{
    public class Point
    {
        [JsonPropertyName("x")]
        public double X { get; set; }

        [JsonPropertyName("y")]
        public double Y { get; set; }
    }
}
