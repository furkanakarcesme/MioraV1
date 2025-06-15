using Entities.Enums;

namespace Entities.Models;

public class LabObservation
{
    public int Id { get; set; }
    public int AnalysisId { get; set; }
    public AnalysisResult Analysis { get; set; }
    public string Name { get; set; }
    public double? Value { get; set; }
    public StatusEnum Status { get; set; }
} 