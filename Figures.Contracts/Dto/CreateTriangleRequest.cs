using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Figures.Contracts.Dto
{
    public class CreateTriangleRequest
    {
        [Required]
        [JsonPropertyName("vertex1")]
        public Point Vertex1 { get; set; }

        [Required]
        [JsonPropertyName("vertex2")]
        public Point Vertex2 { get; set; }

        [Required]
        [JsonPropertyName("vertex3")]
        public Point Vertex3 { get; set; }
    }
}
