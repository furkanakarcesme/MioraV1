using Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class AnalysisResult
{
    public int Id { get; set; }
    public Guid UploadId { get; set; }
    public AnalysisType Type { get; set; }

    [Column(TypeName = "nvarchar(max)")]
    public string RawJson { get; set; }

    [Column(TypeName = "nvarchar(max)")]
    public string? ResultSummary { get; set; }

    public double? Probability { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<LabObservation> LabObservations { get; set; } = new List<LabObservation>();
    public ICollection<AiPromptLog> AiPromptLogs { get; set; } = new List<AiPromptLog>();
} 