namespace Entities.DataTransferObjects;

public record XRayResponseDto(int AnalysisId, string Explanation, IEnumerable<string> Suggestions); 