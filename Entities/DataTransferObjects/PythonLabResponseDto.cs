using System.Text.Json.Serialization;

namespace Entities.DataTransferObjects
{
    public class PythonLabResponseDto
    {
        [JsonPropertyName("lab_values")]
        public Dictionary<string, double> LabValues { get; set; }
    }
} 