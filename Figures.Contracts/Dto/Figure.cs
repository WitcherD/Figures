using System.Text.Json.Serialization;

namespace Figures.Contracts.Dto
{
    public class Figure
    {
        [JsonPropertyName("figure_id")]
        public int FigureId { get; set; }

        [JsonPropertyName("area")]
        public double? Area { get; set; }
    }
}
