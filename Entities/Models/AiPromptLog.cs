using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class AiPromptLog
{
    public int Id { get; set; }
    public int AnalysisId { get; set; }
    public AnalysisResult Analysis { get; set; }

    [Column(TypeName = "nvarchar(max)")]
    public string PromptText { get; set; }

    [Column(TypeName = "nvarchar(max)")]
    public string AiResponse { get; set; }

    public int TokenCost { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
} 