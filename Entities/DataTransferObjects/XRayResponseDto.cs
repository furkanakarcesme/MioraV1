namespace Entities.DataTransferObjects;

public record XRayResponseDto(
    int AnalysisId, 
    string Summary, 
    string Details, 
    string Explanation, 
    IEnumerable<string> Suggestions); 