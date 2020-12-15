using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Figures.Contracts.Dto
{
    public class CreateCircleRequest
    {
        [Required]
        [JsonPropertyName("center")]
        public Point Center { get; set; }

        [Range(1, 1000)]
        [JsonPropertyName("radius")]
        public double Radius { get; set; }
    }
}